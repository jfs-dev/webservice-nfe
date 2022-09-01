using System.Security.Cryptography.X509Certificates;
using System.Text;

const string NOME_CERTIFICADO = "INFORME AQUI SEU CERTIFICADO";

static void StatusServicoNFe()
{
    var handler = new HttpClientHandler();
    handler.ClientCertificates.Add(GetCertificate(NOME_CERTIFICADO));

    var httpClient = new HttpClient(handler);

    //SP
    //var soapXml = @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://www.w3.org/2003/05/soap-envelope""><soap:Header><nfeCabecMsg xmlns=""http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4""><cUF>35</cUF><versaoDados>4.00</versaoDados></nfeCabecMsg></soap:Header><soap:Body><nfeDadosMsg xmlns=""http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4""><consStatServ xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.00""><tpAmb>2</tpAmb><cUF>35</cUF><xServ>STATUS</xServ></consStatServ></nfeDadosMsg></soap:Body></soap:Envelope>";

    // var response = httpClient.PostAsync("https://homologacao.nfe.fazenda.sp.gov.br/ws/nfestatusservico4.asmx", new StringContent(soapXml, Encoding.UTF8, "text/xml")).Result;

    //MG
    var soapXml = @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://www.w3.org/2003/05/soap-envelope""><soap:Header><nfeCabecMsg xmlns=""http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4""><cUF>31</cUF><versaoDados>4.00</versaoDados></nfeCabecMsg></soap:Header><soap:Body><nfeDadosMsg xmlns=""http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4""><consStatServ xmlns=""http://www.portalfiscal.inf.br/nfe"" versao=""4.00""><tpAmb>2</tpAmb><cUF>31</cUF><xServ>STATUS</xServ></consStatServ></nfeDadosMsg></soap:Body></soap:Envelope>";

    var response = httpClient.PostAsync("https://hnfe.fazenda.mg.gov.br/nfe2/services/NFeStatusServico4", new StringContent(soapXml, Encoding.UTF8, "text/xml")).Result;

    var content = response.Content.ReadAsStringAsync().Result;

    Console.WriteLine(content);
}

static X509Certificate2 GetCertificate(string certificateName)
{
    X509Store store = new X509Store(StoreLocation.CurrentUser);
    store.Open(OpenFlags.ReadOnly);
    X509Certificate2 certificate = store.Certificates.Find(X509FindType.FindBySubjectName, certificateName, true)[0];
    store.Close();

    return certificate;
}

Console.WriteLine("Testando comunicação WebService NF-e");
Console.WriteLine("------------------------------------");
StatusServicoNFe();

Console.ReadKey();