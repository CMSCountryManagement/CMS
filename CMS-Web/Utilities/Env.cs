using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CMS_Web.Constants;

namespace CMS_Web.Utilities
{
	public static class Env
	{
		//userid||username||email||firstname||lastname||imagepath||roles
        //userdata = string.Concat(user.ID, "|", user.Username, "|", user.Name, "|", user.Gender, "|", user.Email, "|", "User,", "|", "User,", "|", profileImage, "|");
		//user.Id, "|", user.UserName, "|", user.Email, "|", profileImage, "|", String.Join(",", user.Roles));
		public static string UserId()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[0];
			}
			else
			{
				return "";
			}
		}
		public static string UserName()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[1];

			}
			else
			{
				return "";
			}

		}
		public static string Email()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[2];
			}
			else
			{
				return "";
			}
		}

		public static string Roles()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[4];
			}
			else
			{
				return "";
			}
		}

		public static bool UserIsInRole(string role)
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				role = role.ToLower();
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				if (userDataPieces[4].ToLower().IndexOf(role) >= 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public static string ImagePath()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[5];

			}
			else
			{
				return "";
			}
		}
		public static string[] GetRolesForUser()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[5].Split(CMS_Web.Constants.Constants.PIPE);
			}
			else
			{
				return new[] { "" };
			}
		}

		public static string ProfileImage()
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated)
			{
				FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
				string[] userDataPieces = fi.Ticket.UserData.Split(CMS_Web.Constants.Constants.PIPE);
				return userDataPieces[7];

			}
			else
			{
				return "";
			}

		}
	}
}