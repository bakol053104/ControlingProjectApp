namespace ControlingProjectApp.Components.CsvReader;

public interface IXmlFilesService
{
    void CreateXml(string csvFilePath, string xmlFilePath);

    public List<string> QueryXml(string csvFilePath);

    void InquiryXmlTask(string fuelFilePath, string manufacturerFilePath, string xmlTaskFilePath);
}
