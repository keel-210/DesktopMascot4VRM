using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;
public class Settings4VRM : MonoBehaviour
{
	public AllowedUser APersonWhoCanPerform = AllowedUser.Everyone;
	public UssageLicense violent = UssageLicense.Disallow;
	public UssageLicense sexuality = UssageLicense.Disallow;
	public UssageLicense commercial = UssageLicense.Allow;
}