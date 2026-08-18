// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable enable

namespace Microsoft.AspNetCore.Internal;

internal static class AspNetCoreTempDirectory
{
    private static string? _tempDirectory;

    public static string TempDirectory
    {
        get
        {
            if (_tempDirectory == null)
            {
                // Look for folders in the following order.
                var temp = Environment.GetEnvironmentVariable("ASPNETCORE_TEMP") ?? // ASPNETCORE_TEMP - User set temporary location.
                           Path.GetTempPath();                                      // Fall back.

                _tempDirectory = temp;
            }

            // The path is cached for the process lifetime, but the directory can still disappear
            // (cleanup tools, operators). CreateDirectory is a no-op if it already exists.
            Directory.CreateDirectory(_tempDirectory);

            return _tempDirectory;
        }
    }

    public static Func<string> TempDirectoryFactory => () => TempDirectory;
}
