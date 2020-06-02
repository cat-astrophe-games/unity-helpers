using UnityEngine;
using System.Collections;

namespace CatAstropheGames
{
    internal interface IPlatformPermission
    {
        void CheckPlatform();
        bool HasUserAuthorizedPermission(PermissionName permissionName);
        void RequestUserPermission(PermissionName permissionName);

    }


}