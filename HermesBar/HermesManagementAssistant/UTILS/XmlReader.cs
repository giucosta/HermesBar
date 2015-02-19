using MODEL;
using MODEL.Estabelecimento;
using MODEL.Fornecedor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace UTILS
{
    public class XmlReader
    {
        public XmlDocument LoadXml(string caminhoXml)
        {
            var doc = new XmlDocument();
            doc.Load(caminhoXml);
            return doc;
        }
        public void ImportXml(string filename, Stream Data)
        {
            try
            {
                var path = ImportXmlFromPath(filename, Data);
                if (path != string.Empty)
                    ReadEmitter(path);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string NfeEmitterName(string xml)
        {
            var document = LoadXml(xml);
            return document.GetElementsByTagName("xNome")[0].InnerText.Trim();
        }
        private string NfeDateEmission(string xml)
        {
            var document = LoadXml(xml);
            return document.GetElementsByTagName("dEmi")[0].InnerText.Trim();
        }
        public string ImportXmlFromPath(string filename, Stream Data)
        {
            try
            {
                var directory = "C:\\HermesManagementAssistant\\Xml\\" + NfeEmitterName(filename);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                BinaryReader reader = new BinaryReader(Data);
                string path = directory + "\\" + NfeDateEmission(filename) + ".xml";
                FileStream fstream = new FileStream(path, FileMode.CreateNew);
                BinaryWriter wr = new BinaryWriter(fstream);
                wr.Write(reader.ReadBytes((int)Data.Length));
                wr.Close();
                fstream.Close();
                return path;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public void DeletePathXml(string directory)
        {
            Directory.Delete(directory, true);
        }
        private void ReadEmitter(string filename)
        {
            try
            {
                var doc = LoadXml(filename);
                FornecedorModel fornecedor = new FornecedorModel();
                foreach (XmlNode emit in doc.GetElementsByTagName("emit"))
                {
                    foreach (XmlNode node1 in emit.ChildNodes)
                    {
                        if (node1.Name.Equals("xNome"))
                            fornecedor.RazaoSocial = node1.InnerText;
                        if (node1.Name.Equals("CNPJ"))
                            fornecedor.Cnpj = node1.InnerText;
                        if (node1.Name.Equals("IE"))
                            fornecedor.InscricaoEstadual = node1.InnerText;

                        if (node1.Name.Equals("enderEmit"))
                        {
                            var endereco = new EnderecoModel();
                            foreach (XmlNode enderEmit in node1.ChildNodes)
                            {
                                if (enderEmit.Name.Equals("xLgr"))
                                    endereco.Rua = enderEmit.InnerText;
                                if (enderEmit.Name.Equals("nro"))
                                    endereco.Numero = enderEmit.InnerText;
                            }
                            fornecedor.Endereco = endereco;
                        }
                    }
                }

                //var emitente = LoadXml(filename).GetElementsByTagName("emit");
                //FornecedorModel fornecedor = new FornecedorModel();

                //if (emitente.Count > 0)
                //{
                //    foreach (XmlElement element in emitente)
                //    {
                //        fornecedor.RazaoSocial = element.GetElementsByTagName("xNome")[0].InnerText;
                //        fornecedor.Cnpj = element.GetElementsByTagName("CNPJ")[0].InnerText;
                //        fornecedor.InscricaoEstadual = element.GetElementsByTagName("IE")[0].InnerText;
                //    }
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
