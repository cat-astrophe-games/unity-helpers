using UnityEngine;
using System;

namespace CatAstropheGames
{
    public class IOSPermission : IPlatformPermission
    {
        private IOSPermissionAuthStatus locationAuthStatus = IOSPermissionAuthStatus.NotDetermined;

        private IOSPermission()
        {
            GetAuthStatus();
        }

        private void GetAuthStatus()
        {
            int authLevelInt = LocationManagerBridge.getAuthrizationLevelForApplication();
            locationAuthStatus  = IOSPermissionAuthStatus.FromAuthLevelInt(authLevelInt);
        }

        public void CheckPlatform()
        {
            if (Application.platform != RuntimePlatform.IPhonePlayer)
            {
                throw new Exception("Platform should be iphone, but is: " + Application.platform);
            }
        }

        public bool HasUserAuthorizedPermission(PermissionName permissionName)
        {
            return locationAuthStatus == IOSPermissionAuthStatus.AuthorizedAlways;
        }

        public void RequestUserPermission(PermissionName permissionName)
        {
            if (permissionName == PermissionName.FineLocationAlways)
            {
                LocationManagerBridge.requestAuthorizedAlways();
            } else
            {
                throw new NotSupportedException("Permissions don't support this permission: " + permissionName.GetType().Name);
            }
        }
    }
}