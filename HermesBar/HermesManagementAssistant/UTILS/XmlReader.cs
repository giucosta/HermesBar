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
        public FornecedorModel ImportXml(string filename, Stream Data)
        {
            try
            {
                var path = ImportXmlFromPath(filename, Data);
                if (path != string.Empty)
                    return ReadEmitter(path);
                return null;
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
                {
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
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private FornecedorModel ReadEmitter(string filename)
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
                                if (enderEmit.Name.Equals("xBairro"))
                                    endereco.Bairro = enderEmit.InnerText;
                                if (enderEmit.Name.Equals("xMun"))
                                    endereco.Bairro = enderEmit.InnerText;
                                if (enderEmit.Name.Equals("CEP"))
                                    endereco.Cep = enderEmit.InnerText;
                                if (enderEmit.Name.Equals("UF"))
                                    endereco.Estado = enderEmit.InnerText;
                            }
                            endereco.Tipo = new TipoEnderecoModel() { Tipo = Constantes.ATipoEndereco.MATRIZ };
                            fornecedor.Endereco = endereco;
                        }
                    }
                }
                return fornecedor;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
