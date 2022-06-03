namespace MP.BlazorStateDemo.IntegrationTestsAlt.Extensions;

internal static class Extensions
{
    public static void UpTo(
        this int start,
        int end,
        Action<int> proc)
    {
        for (int i = start; i <= end; i++)
        {
            proc(i);
        }
    }
}