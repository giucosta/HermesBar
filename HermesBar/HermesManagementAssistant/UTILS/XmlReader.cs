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
        private void ReadEmitter(string filename)
        {
            try
            {
                var doc = LoadXml(filename);
                var emitente = doc.GetElementsByTagName("emit");
                FornecedorModel fornecedor = new FornecedorModel();

                foreach (XmlElement element in emitente)
                {
                    fornecedor.RazaoSocial = element.GetElementsByTagName("xNome")[0].InnerText;
                    fornecedor.Cnpj = element.GetElementsByTagName("CNPJ")[0].InnerText;
                    fornecedor.InscricaoEstadual = element.GetElementsByTagName("IE")[0].InnerText;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
