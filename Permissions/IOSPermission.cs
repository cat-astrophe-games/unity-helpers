using UnityEngine;
using System;

namespace CatAstropheGames
{
    /// <summary>
    /// You can only ask about given permissions and show the dialog.
    /// But you don't know whether the user clicked on any option when he saw the system GPS dialog.
    /// </summary>
    public class IOSPermission : IPlatformPermission
    {
        private IOSPermissionAuthStatus locationAuthStatus = IOSPermissionAuthStatus.NotDetermined;

        public IOSPermission()
        {
            GetAuthStatus();
        }

        private void GetAuthStatus()
        {
            int authLevelInt = LocationManagerBridge.getAuthrizationLevelForApplication();
            locationAuthStatus = IOSPermissionAuthStatus.FromAuthLevelInt(authLevelInt);
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
            GetAuthStatus();
            if (permissionName == PermissionName.FineLocationAlways
                && locationAuthStatus == IOSPermissionAuthStatus.AuthorizedAlways)
            {
                return true;
            }

            if (permissionName == PermissionName.LocationWhenInUse
                && (locationAuthStatus == IOSPermissionAuthStatus.AuthorizedWhenInUse || locationAuthStatus == IOSPermissionAuthStatus.AuthorizedAlways))
            {
                return true;
            }
            
            Debug.Log("location auth status: " + locationAuthStatus);

            return false;
        }

        public void RequestUserPermission(PermissionName permissionName)
        {
            if (permissionName == PermissionName.FineLocationAlways)
            {
                LocationManagerBridge.requestAuthorizedAlways();
            } else if (permissionName == PermissionName.LocationWhenInUse)
            {
                LocationManagerBridge.requestAuthorizedWhenInUse();
            } else 
            {
                throw new NotSupportedException("Permissions don't support this permission: " + permissionName.GetType().Name);
            }
        }
    }
}