using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;

namespace LibRSA
{
    public class CriptaDecripta
    {
        BigInteger _e;
        BigInteger _d;
        BigInteger _n;

        List<BigInteger> _vMessaggio;
        List<BigInteger> _c;

        //Costruttore per Criptare
        ///<summary>
        ///Cripta l'Immagine: Messaggio da Criptare, Chiave Pubblica(E, N)
        ///</summary>
        public CriptaDecripta(string messaggio, BigInteger E, BigInteger N)
        {
            this._e = E;
            this._n = N;
            this._vMessaggio = GeneraVMessaggio(messaggio);
            this._c = GeneraC(_vMessaggio, N, E);
        }

        //Costruttore per Decriptare
        ///<summary>
        ///Decripta l'Immagine: Chiave Privata(D, N)
        ///</summary>
        public CriptaDecripta(BigInteger D, BigInteger N)
        {
            this._d = D;
            this._n = N;
        }

        private List<BigInteger> GeneraVMessaggio(string _messaggio)
        {
            List<BigInteger> _v = new List<BigInteger>();
            byte[] _asciiBytes;
            List<int> _asciiInt = new List<int>();

            _asciiBytes = Encoding.ASCII.GetBytes(_messaggio);
            for (int i = 0; i < _asciiBytes.Length; i++)
            {
                _asciiInt.Add(_asciiBytes[i]);
                _v.Add(_asciiInt[i]);
            }

            return _v;
        }

        private List<BigInteger> GeneraC(List<BigInteger> _vMessaggio, BigInteger _n, BigInteger _e)
        {
            List<BigInteger> _c = new List<BigInteger>();

            for (int _i = 0; _i < _vMessaggio.Count; _i++)
                _c.Add((_vMessaggio[_i].modPow(_e, _n)));

            return _c;
        }

        private List<BigInteger> GeneraM(List<BigInteger> _c, BigInteger _n, BigInteger _d)
        {
            List<BigInteger> _decriptato = new List<BigInteger>();

            for (int _i = 0; _i < _c.Count; _i++)
                _decriptato.Add(_c[_i].modPow(_d, _n));

            return _decriptato;
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }

        private string StringaMagica(string s, bool add, int color)
        {
            if (add)
                s += "A";
            else
                s += "S";

            if (color == 1)
                s += "R";
            else if (color == 2)
                s += "G";
            else if (color == 3)
                s += "B";

            s += "%";

            return s;
        }

        public Bitmap CriptaImmagine(Bitmap img, string path)
        {

            #region var
            List<char> _char = new List<char>();
            List<string> _listaMagica = new List<string>();
            int _colorR = 0;
            int _colorG = 0;
            int _colorB = 0;
            int _index = 0;
            bool _endOfMsg = false;
            string _stringaMagica = "";

            //Random
            Random rdmColore = new Random(DateTime.Now.Millisecond);
            Random rdmOperazione = new Random(DateTime.Now.Millisecond);

            Random rdmX = new Random(DateTime.Now.Millisecond);
            Random rdmY = new Random(DateTime.Now.Millisecond);

            int coloreRdm = 0;
            int operazioneRdm = 0;
            int x = 0;
            int y = 0;
            int k = -1;
            #endregion

            for (int i = 0; i < _c.Count; i++)
            {
                for (int j = 0; j < _c[i].ToString().Length; j++)
                {
                    string _s = _c[i].ToString();
                    _char.Add(_s[j]);

                }
                _char.Add('E');
            }

            while (k < _char.Count)
            {
                k++;
                x = rdmX.Next(0, img.Width);
                y = rdmY.Next(0, img.Height);
                _stringaMagica = string.Format("{0},{1}%", x, y);

                Color _getColor = img.GetPixel(x, y);

                _colorR = _getColor.R;
                _colorG = _getColor.G;
                _colorB = _getColor.B;

                if (k == _char.Count - 1)
                    _endOfMsg = true;

                if (_endOfMsg == false)
                {
                    if (_char[k] != 'E')
                    {
                        while (_index < Convert.ToInt32(_char[k].ToString()))
                        {
                            if (_index == Convert.ToInt32(_char[k].ToString())) { break; }

                            //1-Rosso, 2-Verde, 3-Blu 
                            coloreRdm = rdmColore.Next(1, 4);
                            //0-Sottrazione, 1-Somma
                            operazioneRdm = rdmOperazione.Next(0, 2);

                            if (coloreRdm == 1)
                            {
                                if (_colorR == 0)
                                {
                                    _colorR++;
                                    _index++;
                                    _stringaMagica = StringaMagica(_stringaMagica, true, 1);
                                }

                                else if (_colorR == 255)
                                {
                                    _colorR--;
                                    _index++;
                                    _stringaMagica = StringaMagica(_stringaMagica, false, 1);
                                }

                                else if (_colorR >= 1 && _colorR <= 254)
                                {
                                    if (operazioneRdm == 1)
                                    {
                                        _colorR++;
                                        _index++;
                                        _stringaMagica = StringaMagica(_stringaMagica, true, 1);
                                    }
                                    else
                                    {
                                        _colorR--;
                                        _index++;
                                        _stringaMagica = StringaMagica(_stringaMagica, false, 1);
                                    }

                                }
                            }

                            if (_index == Convert.ToInt32(_char[k].ToString())) { break; }

                            if (coloreRdm == 2)
                            {
                                if (_colorG == 0)
                                {
                                    _colorG++;
                                    _index++;
                                    _stringaMagica = StringaMagica(_stringaMagica, true, 2);
                                }

                                else if (_colorG == 255)
                                {
                                    _colorG--;
                                    _index++;
                                    _stringaMagica = StringaMagica(_stringaMagica, false, 2);
                                }

                                else if (_colorG >= 1 && _colorG <= 254)
                                {
                                    if (operazioneRdm == 1)
                                    {
                                        _colorG++;
                                        _index++;
                                        _stringaMagica = StringaMagica(_stringaMagica, true, 2);
                                    }
                                    else
                                    {
                                        _colorG--;
                                        _index++;
                                        _stringaMagica = StringaMagica(_stringaMagica, false, 2);
                                    }

                                }
                            }

                            if (_index == Convert.ToInt32(_char[k].ToString())) { break; }

                            if (coloreRdm == 3)
                            {
                                if (_colorB == 0)
                                {
                                    _colorB++;
                                    _index++;
                                    _stringaMagica = StringaMagica(_stringaMagica, true, 3);
                                }

                                else if (_colorB == 255)
                                {
                                    _colorB--;
                                    _index++;
                                    _stringaMagica = StringaMagica(_stringaMagica, false, 3);
                                }

                                else if (_colorB >= 1 && _colorB <= 254)
                                {
                                    if (operazioneRdm == 1)
                                    {
                                        _colorB++;
                                        _index++;
                                        _stringaMagica = StringaMagica(_stringaMagica, true, 3);
                                    }
                                    else
                                    {
                                        _colorB--;
                                        _index++;
                                        _stringaMagica = StringaMagica(_stringaMagica, false, 3);
                                    }

                                }
                            }

                            if (_index == Convert.ToInt32(_char[k].ToString())) { break; }
                        }
                    }

                    _stringaMagica += ";";
                    _index = 0;

                    if (!_endOfMsg)
                        _listaMagica.Add(_stringaMagica);

                    Color _newPixelColor = Color.FromArgb(_colorR, _colorG, _colorB);
                    img.SetPixel(x, y, _newPixelColor);
                }
            }

            Bitmap immagineCriptata;
            immagineCriptata = new Bitmap(img);
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            //img.Dispose();
            immagineCriptata.Save(path, jpgEncoder, myEncoderParameters);
            //immagineCriptata.Dispose();

            StreamWriter sw = new StreamWriter(path, true);

            sw.WriteLine("");
            sw.WriteLine("[H]");

            List<BigInteger> _listVMesaggio = new List<BigInteger>();
            List<BigInteger> _listC = new List<BigInteger>();

            for (int i = 0; i < _listaMagica.Count; i++)
            {
                _listVMesaggio = GeneraVMessaggio(_listaMagica[i]);
                _listC = GeneraC(_listVMesaggio, _n, _e);
                for (int j = 0; j < _listC.Count; j++)
                    sw.WriteLine("{0}", _listC[j]);
            }

            BigInteger msgCount = _c.Count;
            msgCount = msgCount.modPow(_e, _n);
            sw.WriteLine(msgCount.ToString());

            sw.WriteLine("[EOH]");

            sw.WriteLine("[C]");

            for (int i = 0; i < _c.Count; i++)
                sw.WriteLine(_c[i].ToString().Length);

            sw.WriteLine("[EOC]");
            sw.Close();

            return immagineCriptata;
        }

        public string DecriptaImmagine(Bitmap _imgCript, string path)
        {

            #region var
            StreamReader sr = new StreamReader(path);
            bool _hMessaggio = false;
            bool _cMessaggio = false;
            List<BigInteger> s = new List<BigInteger>();
            List<BigInteger> cHidden = new List<BigInteger>();
            List<BigInteger> bigDecripto = new List<BigInteger>();
            List<string> sDecripto = new List<string>();
            List<string> splitDecriptato = new List<string>();
            string read = "";
            int split = 59;
            string sMagica = "";
            int j = 0;
            string pixel = "";
            string[] pixelSplit = new string[2];
            char color;
            char operazione;
            string messaggio1 = "";
            string msg = "";

            int _colorR = 0;
            int _colorG = 0;
            int _colorB = 0;

            List<System.Numerics.BigInteger> bigConverto = new List<System.Numerics.BigInteger>();
            System.Numerics.BigInteger bIntegerConverto = 0;
            List<System.Numerics.BigInteger> listDecriptato = new List<System.Numerics.BigInteger>();
            System.Numerics.BigInteger bigD = 0;
            System.Numerics.BigInteger bigN = 0;
            #endregion

            while (!sr.EndOfStream)
            {
                read = sr.ReadLine();
                if (read == "[H]" || _hMessaggio)
                {
                    _hMessaggio = true;
                    try { s.Add(Convert.ToInt64(read)); } catch (Exception e) { }
                }
                if (read == "[C]" || _cMessaggio)
                {
                    _cMessaggio = true;
                    try { cHidden.Add(Convert.ToInt64(read)); } catch (Exception e) { }
                }
                if (read == "[EOC]")
                {
                    sr.Close();
                    break;
                }

            }

            bigDecripto = GeneraM(s, _n, _d);
            for (int i = 0; i < bigDecripto.Count; i++)
            {
                try
                {
                    while (bigDecripto[j] != split)
                    {
                        string sc = bigDecripto[j].ToString();
                        char c = (char)(Convert.ToInt32(sc));
                        sMagica += c;
                        j++;
                    }
                    j++;
                    sDecripto.Add(sMagica.ToString());
                    sMagica = "";
                }
                catch (Exception e) { break; };
            }

            for (int i = 0; sDecripto.Count > 0; i++)
            {
                for (int k = 0; k < cHidden[i]; k++)
                {
                    splitDecriptato = sDecripto[0].Split('%').ToList();
                    pixel = splitDecriptato[0];
                    splitDecriptato.RemoveAt(0);
                    splitDecriptato.RemoveAt(splitDecriptato.Count - 1);
                    messaggio1 += splitDecriptato.Count.ToString();


                    //
                    //IN ALPHA
                    //

                    //for (int h = 0; h < splitDecriptato.Count; h++)
                    //{
                    //    operazione = splitDecriptato[h][0];
                    //    color = splitDecriptato[h][1];
                    //    pixelSplit = pixel.Split(',');

                    //    Color _getColor = _imgCript.GetPixel(Convert.ToInt32(pixelSplit[0]), Convert.ToInt32(pixelSplit[1]));

                    //    _colorR = _getColor.R;
                    //    _colorG = _getColor.G;
                    //    _colorB = _getColor.B;

                    //    if(operazione == 'A')
                    //    {
                    //        if (color == 'R')
                    //            _colorR--;
                    //        else if (color == 'G')
                    //            _colorG--;
                    //        else if (color == 'B')
                    //            _colorB--;
                    //    }
                    //    else
                    //    {
                    //        if (color == 'R')
                    //            _colorR++;
                    //        else if (color == 'G')
                    //            _colorG++;
                    //        else if (color == 'B')
                    //            _colorB++;
                    //    }
                    //}

                    //Color colorOriginal = Color.FromArgb(_colorR, _colorG, _colorB);
                    //_imgCript.SetPixel(Convert.ToInt32(pixelSplit[0]), Convert.ToInt32(pixelSplit[1]), colorOriginal);
                    //


                    if ((splitDecriptato.Count.ToString() == "0") && (k == 0))
                    {
                        messaggio1 = "";
                        k--;
                    }
                    sDecripto.RemoveAt(0);
                    if (messaggio1.Length == cHidden[i])
                        break;

                }

                System.Numerics.BigInteger.TryParse(messaggio1, out bIntegerConverto);
                bigConverto.Add(bIntegerConverto);
                bIntegerConverto = 0;
                messaggio1 = "";
            }

            System.Numerics.BigInteger.TryParse(_d.ToString(), out bigD);
            System.Numerics.BigInteger.TryParse(_n.ToString(), out bigN);

            for (int i = 0; i < bigConverto.Count; i++)
            {
                bIntegerConverto = System.Numerics.BigInteger.ModPow(bigConverto[i], bigD, bigN);
                string z = bIntegerConverto.ToString();
                long bau = Convert.ToInt32(z);
                char c = Convert.ToChar(bau);
                msg += c;
            }

            return msg;
        }
    }
}
