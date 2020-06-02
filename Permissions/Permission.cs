using strange.extensions.promise.api;
using strange.extensions.promise.impl;
using System;
using UnityEngine;

namespace CatAstropheGames
{
    /// <summary>
    /// This requires: https://assetstore.unity.com/packages/tools/integration/location-manager-plugin-for-ios-96860
    /// </summary>
    public static class Permission
    {
        private static IPlatformPermission _platformPermission;

        private static IPlatformPermission platformPermission
        {
            get
            {
                if (_platformPermission == null)
                {
                    IPlatformPermission permission;
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            permission = new AndroidPermission();
                            break;
                        case RuntimePlatform.WindowsEditor:
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.WindowsPlayer:
                            permission = new EditorPermission();
                            break;
                        case RuntimePlatform.IPhonePlayer:
                            permission = new IOSPermission();
                            break;
                        default:
                            throw new NotImplementedException("platform " + Application.platform + " not supported yet.");
                    }

                    _platformPermission = permission;
                }

                return _platformPermission;
            }
        }

        internal static void RequestUserPermission(PermissionName permissionName)
        {
            platformPermission.RequestUserPermission(permissionName);
        }

        public static bool HasUserAuthorizedPermission(PermissionName permissionName)
        {
            return platformPermission.HasUserAuthorizedPermission(permissionName);
        }
    }
}