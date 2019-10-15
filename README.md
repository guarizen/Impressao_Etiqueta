# Impressao_Etiqueta
Impressão de Etiquetas - Zebra



 IWin32Window SendStringToPrinter = null;
                        if (DialogResult.OK == pd.ShowDialog(SendStringToPrinter))
                        {
                            RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, s);
                         }
                        else
                        {
                            Console.WriteLine("Cancelada Impressão!");
                        }
