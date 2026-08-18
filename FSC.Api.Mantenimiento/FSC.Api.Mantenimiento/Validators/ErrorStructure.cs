namespace FSC.Api.Mantenimiento.Errors
{
    
    public class ErrorStructure
    {
        public ErrorStructure()
        {
            this.Details = new List<DetailError>();
        }
        public ErrorStructure(String message)
        {
            this.Details = new List<DetailError>();
            this.Details.Add(new DetailError() { Detail = message });
        }
        public Int32 StatusCode {  get; set; }
        public string Message { get; set; } = "Errores de validación de datos.";
        public List<DetailError> Details { get; set; }
    }
    public class DetailError
    {
        public string Detail { get; set; }
    }
}
