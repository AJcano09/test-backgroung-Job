namespace test_background_api.dbContext.Helper;

public class MigrationUtility
{
    public static string ReadSql(Type migrationType, string sqlFileName)
    {
        var assembly = migrationType.Assembly;
        var resourceName = $"{migrationType.Namespace}.{sqlFileName}";
        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == null)
            throw new FileNotFoundException("Unable to find the SQL file from an embedded resource", resourceName);

        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();
        return content;
    }
}