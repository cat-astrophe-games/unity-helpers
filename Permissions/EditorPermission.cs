using UnityEngine;
using System;

namespace CatAstropheGames
{
    public class EditorPermission : IPlatformPermission
    {
        public void CheckPlatform()
        {
            if (Application.platform != RuntimePlatform.OSXEditor || Application.platform != RuntimePlatform.WindowsEditor || Application.platform != RuntimePlatform.WindowsPlayer)
            {
                throw new Exception("Platform should be editor, but is: " + Application.platform);
            }

        }

        public string GetPermissionString(PermissionName permissionName)
        {
            return $"<editor-permission-string-{permissionName.GetType().Name}";
        }

        public bool HasUserAuthorizedPermission(PermissionName permissionName)
        {
            return true;
        }

        public void RequestUserPermission(PermissionName permissionName)
        {

        }
    }

}
