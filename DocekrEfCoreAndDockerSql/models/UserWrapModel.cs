using System.Collections.Generic;

namespace DocekrEfCoreAndDockerSql.models {
	public class UserWrapModel {

		#region // properties //
		public List<UserModel> users { get; set; } = new List<UserModel>();
		#endregion

	}
}
