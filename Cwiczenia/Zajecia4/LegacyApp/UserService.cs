using System;

namespace LegacyApp {
	public class UserService {
		public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId) {

			if (!IsNameValid(firstName, lastName)) return false;
			if (!IsEmailValid(email)) return false;
			if (!IsAgeValid(dateOfBirth)) return false;

			var client = GetClientById(clientId);
			if (client == null) return false;

			var user = CreateUser(firstName, lastName, email, dateOfBirth, client);
			if (!IsCreditLimitValid(user)) return false;

			UserDataAccess.AddUser(user);
			return true;
		}
		private bool IsNameValid(string firstName, string lastName) {
			return !(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName));
		}
		private bool IsEmailValid(string email) {
			return email.Contains("@") && email.Contains(".");
		}
		private bool IsAgeValid(DateTime dateOfBirth) {
			int age = GetCurrentAge(dateOfBirth);
			return age >= 21;
		}
		private int GetCurrentAge(DateTime dateOfBirth) {
			DateTime now = DateTime.Now;
			int age = now.Year - dateOfBirth.Year;
			if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
			return age;
		}
		private bool IsCreditLimitValid(User user) {
			return !user.HasCreditLimit || user.CreditLimit >= 500;
		}
		private Client GetClientById(int clientId) {
			ClientRepository clientRepository = new ClientRepository();
			return clientRepository.GetById(clientId);
		}
		private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client) {
			User user = new User {
				Client = client,
				DateOfBirth = dateOfBirth,
				EmailAddress = email,
				FirstName = firstName,
				LastName = lastName
			};
			SetUserCreditLimit(user, client);
			return user;
		}
		private void SetUserCreditLimit(User user, Client client) {
			if (client.Type == "VeryImportantClient") user.HasCreditLimit = false;
			else {
				user.HasCreditLimit = true;
				using (var userCreditService = new UserCreditService()) {
					int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
					if (client.Type == "ImportantClient") creditLimit *= 2;
					user.CreditLimit = creditLimit;
				}
			}
		}
	}
}
