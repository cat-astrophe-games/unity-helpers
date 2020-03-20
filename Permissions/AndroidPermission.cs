using UnityEngine;
using System;

namespace CatAstropheGames
{
    internal class AndroidPermission : IPlatformPermission
    {
        public bool HasUserAuthorizedPermission(PermissionName permissionName)
        {
            CheckPlatform();
            return UnityEngine.Android.Permission.HasUserAuthorizedPermission(GetPermissionString(permissionName));
        }

        public void CheckPlatform()
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                throw new Exception("Platform should be Android, but is: " + Application.platform);
            }
        }

        public void RequestUserPermission(PermissionName permissionName)
        {
            CheckPlatform();
            UnityEngine.Android.Permission.RequestUserPermission(GetPermissionString(permissionName));
        }

        private string GetPermissionString(PermissionName permissionName)
        {
            if (permissionName == PermissionName.FineLocationAlways)
            {
                return UnityEngine.Android.Permission.FineLocation;
            }
            throw new NotSupportedException("Permission name: " + permissionName.GetType().Name + " is not supported");

        }
    }
}