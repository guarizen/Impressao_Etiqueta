using System;
using System.Net;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Impressao_Etiquetas
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.Write("Codigo Filial: ");
                string cod_filial = Convert.ToString(Console.ReadLine());
                Console.Write("Documento: ");
                string documento = Convert.ToString(Console.ReadLine());

                var enderecoapi = "http://189.113.4.250:888/api/millenium!pillow/movimentacao/lista_recebimento?romaneio=" + $"{documento}" + "&cod_filial=" + $"{cod_filial}" + "&$format=json";

                var requisicaoWeb = WebRequest.CreateHttp($"{enderecoapi}");
                requisicaoWeb.Method = "GET";
                requisicaoWeb.Headers.Add("Authorization", $"Basic YWRtaW5pc3RyYXRvcjp2dGFUUFJAMjAxOSoq");
                requisicaoWeb.UserAgent = "Consulta API - Recebimento Etiquetas";
                requisicaoWeb.Timeout = 130000;

                Console.WriteLine("Aguardando Retorno do Millennium...");
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    object objResponse = reader.ReadToEnd();
                    var statusCodigo = ((System.Net.HttpWebResponse)resposta).StatusCode;

                    ListaRecebimento Receb = JsonConvert.DeserializeObject<ListaRecebimento>(objResponse.ToString());

                    Console.WriteLine("Processando...");
                    foreach (var reb in Receb.Value)
                    {
                        //Lista de API criar foreach()
                        string s = "^XA" +
                                   "^CF0,30" +
                                   "^FO10,40^FDPILLOWTEX IND. COM. TEXTIL - LTDA^FS" +
                                   "^CFA,30" +
                                   "^CF0,20" +
                                   $"^FO40,220^FDCodigo: {reb.CodProduto} ^FS" +
                                   $"^FO40,250^FDCor: {reb.Cor}^FS" +
                                   $"^FO40,280^FDEstampa: {reb.Estampa}^FS" +
                                   $"^FO40,310^FDTam: {reb.Tamanho}^FS" +
                                   "^CF0,20" +
                                   $"^FO26,130^FD{reb.Descricao1}^FS" +
                                   "^CFA,15" +
                                   "^BY2,1,100^BEN,100,Y,N" +
                                   $"^FO80,360^BC^FD{reb.Ean13}^FS" +
                                   "^XZ";


                        for(int i = 1; reb.Quantidade >= i; i++)
                        {
                            PrintDialog pd = new PrintDialog();
                            pd.PrinterSettings = new PrinterSettings();
                            RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s);
                        }
                        
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}