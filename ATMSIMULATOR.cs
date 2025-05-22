using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

        class Program
        {
            static void Main(string[] args)
            {
                ATM atm = new ATM();
                atm.Run();
            }
        }

        public class Account
        {
            public string AccountId { get; set; }
            public string UserName { get; set; }
            public decimal Balance { get; set; }

            public Account(string accountId, string userName)
            {
                AccountId = accountId;
                UserName = userName;
                Balance = 0.0m;
            }
        }

        public class ATM
        {
            private Account[] accounts;
            private int accountCount;

            public ATM()
            {
                accounts = new Account[10];
                accountCount = 0;
            }

            public void Run()
            {
                int choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("ATM Simulator");
                    Console.WriteLine("1. Create Account");
                    Console.WriteLine("2. Access Account");
                    Console.WriteLine("3. Exit");
                    Console.Write("Choose an option: ");

                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        Console.ReadKey();
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            CreateAccount();
                            break;
                        case 2:
                            AccessAccount();
                            break;
                        case 3:
                            Console.WriteLine("Thank you for using the ATM. Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                } while (choice != 3);
            }

            private void CreateAccount()
            {
                if (accountCount >= accounts.Length)
                {
                    Console.WriteLine("Maximum number of accounts reached.");
                    return;
                }

                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                string accountId = Guid.NewGuid().ToString().Substring(0, 8);
                accounts[accountCount++] = new Account(accountId, name);
                Console.WriteLine($"Account created successfully! Your Account ID is: {accountId}");
            }

            private void AccessAccount()
            {
                Console.Write("Enter your Account ID: ");
                string inputId = Console.ReadLine();
                Account account = FindAccount(inputId);

                if (account != null)
                {
                    int choice;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"Welcome {account.UserName}. Your current balance is: {account.Balance:C}");
                        Console.WriteLine("1. Check Balance");
                        Console.WriteLine("2. Deposit Money");
                        Console.WriteLine("3. Withdraw Money");
                        Console.WriteLine("4. Logout");
                        Console.Write("Choose an option: ");

                        if (!int.TryParse(Console.ReadLine(), out choice))
                        {
                            Console.WriteLine("Invalid input.");
                            Console.ReadKey();
                            continue;
                        }

                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine($"Your balance is: {account.Balance:N2}");
                                break;
                            case 2:
                                Deposit(account);
                                break;
                            case 3:
                                Withdraw(account);
                                break;
                            case 4:
                                Console.WriteLine("Logging out...");
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    } while (choice != 4);
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }

            private Account FindAccount(string accountId)
            {
                for (int i = 0; i < accountCount; i++)
                {
                    if (accounts[i].AccountId == accountId)
                    {
                        return accounts[i];
                    }
                }
                return null;
            }

            private void Deposit(Account account)
            {
                Console.Write("Enter amount to deposit: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    account.Balance += amount;
                    Console.WriteLine($"Successfully deposited {amount:C}. New balance is: {account.Balance:C}");
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }

            private void Withdraw(Account account)
            {
                Console.Write("Enter amount to withdraw: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
                {
                    if (amount <= account.Balance)
                    {
                        account.Balance -= amount;
                        Console.WriteLine($"Successfully withdrew {amount:C}. New balance is: {account.Balance:C}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount.");
                }
            }
        }
    }



