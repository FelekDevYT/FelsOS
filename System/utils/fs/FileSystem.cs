using System;
using System.IO;
using FenixOS.System.utils;

namespace FenixOS.System.modes.cli;

public class FileSystem
{
    public String currentDir = "0:\\";
    private Cosmos.System.FileSystem.CosmosVFS fileSystemInstance = new Cosmos.System.FileSystem.CosmosVFS();//DO NOT EDIT ANYWHERE!

    public void registerFileSystem()
    {
        Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fileSystemInstance);
    }
    
    public FSCode createFile(String path)
    {
        if (File.Exists(getPath(path)))
        {
            return new FSCode(1);
        } else if (Directory.Exists(getPath(path)))
        {
            return new FSCode(2);
        }
        
        fileSystemInstance.CreateFile(getPath(path));
        return new FSCode(0);
    }

    public FSCode remove(String path, bool force)
    {
        //1. detect file or dir
        //2. if dir & force -> remove | !force ->> message
        bool isFile = false;

        if (File.Exists(getPath(path)))
        {
            isFile = true;
        }
        else if (Directory.Exists(getPath(path)))
        {
            isFile = false;
        }
        else
        {
            return new FSCode(1);
        }

        if (isFile)
        {
            File.Delete(getPath(path));
        }
        else
        {
            if (Directory.GetDirectories(getPath(path)).Length == 0 && Directory.GetFiles(getPath(path)).Length == 0)
            {
                if (!force)
                {
                    IO.Debug.error("You should activate force mode.");//CODES IN NEXT UPDATE
                    return new FSCode(2);
                }
            }
            
            Directory.Delete(getPath(path), true);
        }
        
        return new FSCode(0);
    }

    public void changeDirectory(String path)
    {
        string newPath = getPath(path);
        if (Directory.Exists(newPath))
        {
            currentDir = newPath;
            if (!currentDir.EndsWith("\\"))
                currentDir += "\\";
        }
        else
        {
            IO.Debug.error($"Directory not found: {path}");
        }
    }

    public FSCode createDirectory(String path)
    {
        if (File.Exists(getPath(path)))
        {
            return new FSCode(1);
        } else if (Directory.Exists(getPath(path)))
        {
            return new FSCode(2);
        }
        
        fileSystemInstance.CreateDirectory(getPath(path));
        return new FSCode(0);
    }

    public FSCode writeFile(String path, String content)
    {
        try
        {
            File.WriteAllText(getPath(path), content);
        }
        catch (Exception e)
        {
            IO.Debug.error("Error writing file: " + e);
            return new FSCode(e.GetHashCode());//TODO: maybe custom error(in 1.0 or later), not needed rn
        }
        
        return new FSCode(0);
    }

    public String getPath(String path)
    {
        if (String.IsNullOrEmpty(path))
        {
            return currentDir;
        }

        if (path.Contains(@"0:\"))
        {
            return path;
        } else if (path == "..")
        {
            String[] parts = currentDir.Split("\\");
            if (parts.Length > 2)
            {
                String newPath = String.Join("\\", parts, 0, parts.Length - 2);
                return newPath + "\\";
            }

            return "0:\\";
        } else if (path == ".")
        {
            return currentDir;
        }
        else
        {
            String clean = path.TrimStart('\\').TrimEnd('\\');
            if (currentDir.EndsWith("\\"))
            {
                return currentDir + clean;
            }
            else
            {
                return currentDir + "\\" + clean;
            }
        }
    }
    
    public String getFileSystemType(string drive = "0:\\") {
        return fileSystemInstance.GetFileSystemType(drive);
    }

    public long getAvailableFreeSpace(string drive = "0:\\") {
        return fileSystemInstance.GetAvailableFreeSpace(drive);
    }

    public long getTotalSize(string drive = "0:\\") {
        return fileSystemInstance.GetTotalSize(drive);
    }
}