using UnityEngine;
using System.Collections;
using System;

namespace CatAstropheGames
{
    public class IOSPermissionAuthStatus
    {
        public readonly static IOSPermissionAuthStatus NotDetermined = new IOSPermissionAuthStatus();
        public readonly static IOSPermissionAuthStatus Restricted = new IOSPermissionAuthStatus();
        public readonly static IOSPermissionAuthStatus Denied = new IOSPermissionAuthStatus();
        public readonly static IOSPermissionAuthStatus AuthorizedWhenInUse = new IOSPermissionAuthStatus();
        public readonly static IOSPermissionAuthStatus AuthorizedAlways = new IOSPermissionAuthStatus();


        private IOSPermissionAuthStatus()
        {

        }

        internal static IOSPermissionAuthStatus FromAuthLevelInt(int authLevelInt)
        {
            //we sure about those integers?
            // https://developer.apple.com/documentation/corelocation/clauthorizationstatus/kclauthorizationstatusauthorizedwheninuse?language=objc
            switch (authLevelInt)
            {
                case 0:
                    return NotDetermined;
                case 1:
                    return Restricted;
                case 2:
                    return Denied;
                case 3:
                    return AuthorizedWhenInUse;
                case 4:
                    return AuthorizedAlways;
                default:
                    throw new NotSupportedException("unknown auth level :" +authLevelInt);
            }
        }
    }
}
