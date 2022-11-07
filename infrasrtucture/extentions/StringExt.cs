namespace PISWF.infrasrtucture.extentions;

public static class StringExt
{
    public static string Replace(this string str, char[] srs, char dest)
    {
        foreach (var chr in srs)
        {
            str = str.Replace(chr, dest);
        }

        return str;
    }
    
    public static string GetDirectory(this string path)
    {
        var directory = path.Split("\\");
        directory = directory.Take(directory.Length - 1).ToArray();
        return path.Contains('.') ? path : String.Join("\\", directory);
    }
}