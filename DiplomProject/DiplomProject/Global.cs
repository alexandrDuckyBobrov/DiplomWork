using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomProject
{
	public class Global
	{
		public static bool Logged;
        public int fileCount = 0;

		public int GetCount()
		{
			fileCount++;
			return fileCount;
		}
    }
	public class AccountManager
	{
		public static int? Role;
		public static int Id;
		public static string Username;


		public static void Login(int id, string login, int? role)
		{
			Id = id;
			Role = role;
			Username = login;
			Global.Logged = true;
		}
		public static void Logout()
		{
			Id = 0;
			Role = null;
			Username = string.Empty;
			Global.Logged = false;
		}

	}
}