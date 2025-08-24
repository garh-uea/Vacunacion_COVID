using iTextSharp.text;
using iTextSharp.text.pdf;  // Libreria para generar el reporte en PDF

// Clase principal que gestiona la campaña de vacunación
public class CampaniaVacunacion
{
    private HashSet<string> ciudadanos;          // Conjunto total de ciudadanos
    private HashSet<string> pfizer;              // Conjunto de vacunados con Pfizer
    private HashSet<string> astrazeneca;         // Conjunto de vacunados con AstraZeneca

    public CampaniaVacunacion()
    {
        ciudadanos = new HashSet<string>();
        pfizer = new HashSet<string>();
        astrazeneca = new HashSet<string>();
    }

    // Método para generar datos ficticios
    public void GenerarDatos()
    {
        // Se crean 500 ciudadanos ficticios
        for (int i = 1; i <= 500; i++)
        {
            ciudadanos.Add("Ciudadano_" + i);
        }

        // Se eligen 75 ciudadanos ficticios vacunados con Pfizer
        for (int i = 1; i <= 75; i++)
        {
            pfizer.Add("Ciudadano_" + i);
        }

        // Se eligen 75 ciudadanos ficticios vacunados con AstraZeneca
        for (int i = 50; i < 125; i++) // Se incluye algunos ciudadanos con Pfizer
        {
            astrazeneca.Add("Ciudadano_" + i);
        }
    }

    // Método para obtener los listados aplicando operaciones de conjuntos
    public void GenerarReportePDF(string rutaArchivo)
    {
        // Operaciones de teoría de conjuntos
        var noVacunados = new HashSet<string>(ciudadanos);
        noVacunados.ExceptWith(pfizer);
        noVacunados.ExceptWith(astrazeneca);

        var ambasDosis = new HashSet<string>(pfizer);
        ambasDosis.IntersectWith(astrazeneca);

        var soloPfizer = new HashSet<string>(pfizer);
        soloPfizer.ExceptWith(astrazeneca);

        var soloAstrazeneca = new HashSet<string>(astrazeneca);
        soloAstrazeneca.ExceptWith(pfizer);

        // Creación del PDF
        Document documento = new Document();
        PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));
        documento.Open();

        // Título
        documento.Add(new Paragraph("REPORTE DE CAMPAÑA DE VACUNACIÓN COVID-19"));
        documento.Add(new Paragraph("Ministerio de Salud - Gobierno Nacional"));
        documento.Add(new Paragraph("========================================\n"));

        // Ciudadanos no vacunados
        documento.Add(new Paragraph("1. Ciudadanos No vacunados: " + noVacunados.Count));
        foreach (var c in noVacunados)
            documento.Add(new Paragraph(" - " + c));
        documento.Add(new Paragraph("\n"));

        // Ciudadanos con ambas dosis
        documento.Add(new Paragraph("2. Ciudadanos con ambas dosis: " + ambasDosis.Count));
        foreach (var c in ambasDosis)
            documento.Add(new Paragraph(" - " + c));
        documento.Add(new Paragraph("\n"));

        // Solo Pfizer
        documento.Add(new Paragraph("3. Ciudadanos con solo Pfizer: " + soloPfizer.Count));
        foreach (var c in soloPfizer)
            documento.Add(new Paragraph(" - " + c));
        documento.Add(new Paragraph("\n"));

        // Solo AstraZeneca
        documento.Add(new Paragraph("4. Ciudadanos con solo AstraZeneca: " + soloAstrazeneca.Count));
        foreach (var c in soloAstrazeneca)
            documento.Add(new Paragraph(" - " + c));
        documento.Add(new Paragraph("\n"));

        documento.Close();

        Console.WriteLine("Reporte generado en: " + rutaArchivo);
    }
}

class ProgramaVacunacion
{
    static void Main()
    {
        // Se crea el objeto que maneja la campaña
        CampaniaVacunacion campania = new CampaniaVacunacion();

        // Generar los datos ficticios
        campania.GenerarDatos();

        // Generar el reporte en PDF
        string ruta = "ReporteVacunacion.pdf";
        campania.GenerarReportePDF(ruta);
    }
}
