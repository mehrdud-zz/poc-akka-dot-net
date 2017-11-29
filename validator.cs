// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankValidator_HK.cs" company="WorldPay AP Limited">
//   Copyright 2006-2015, WorldPay AP Limited. All rights reserved.
// </copyright>
//
// History 
// -------
// 28/03/2011 CKP created HongKong Validator
// 04/10/2016 TW WPAPE-1383 Made Branch Code mandatory
// 04/10/2016 TW WPAPE-1433 Made Bank Code mandatory
// --------------------------------------------------------------------------------------------------------------------

namespace EPACS.BankValidateResource.BankValidators
{
    using System.Text.RegularExpressions;
    using Common_PayOutResource;
    using WP.Bankouts.Validation.ValidationRules;
    using System.Collections.Generic;
    using WP.Bankouts.Validation.BankValidators.Mehrdad.Rules;

    public class BankValidator_HK_MN : BankValidatorBase, IBankValidator
    {
        private const string PayeeNamePattern = @"[a-zA-Z0-9_,-/:?+\(\)]+";

        private bool checkAccountNumber = true;
        private int accountNumberMinLength = 6;
        private int accountNumberMaxLength = 12;
        private string accountNumberType = "n"; // n | a | an | <blank=an>

        private bool checkBankCode = true;
        private int bankCodeLength = 3;
        private string bankCodeType = "n"; // n | a | an | <blank=an>

        private bool checkBranchCode = true;
        private int branchCodeLength = 3;
        private string branchCodeType = "n"; // n | a | an | <blank=an>


        public List<WP.Bankouts.Validation.BankValidators.Mehrdad.Rules.IValidationRule> ValidationRulesList;
        public BankValidator_HK_MN(BankDetails bankDetails)
            : base(bankDetails)
        {
            ValidationRulesList = new List<WP.Bankouts.Validation.BankValidators.Mehrdad.Rules.IValidationRule>();
            ValidationRulesList.Add(new AccountNumberValidationRule(accountNumberMinLength, accountNumberMaxLength, accountNumberType, checkAccountNumber));
            ValidationRulesList.Add(new BankCodeValidationRule(bankCodeLength, bankCodeType, checkBankCode));
            ValidationRulesList.Add(new BranchCodeValidationRule(branchCodeLength, branchCodeType, checkBranchCode));
            ValidationRulesList.Add(new PayeeValidationRule(true));
            ValidationRulesList.Add(new PayeeNamePatternRule(PayeeNamePattern, true));
        }

        /// <summary>
        /// At this moment, system only checks Account Number is numeric or not.
        /// </summary>
        /// <returns></returns>
        public int Validate()
        {
            // Perform the minimum check - that we have a bank account number
            foreach (WP.Bankouts.Validation.BankValidators.Mehrdad.Rules.IValidationRule validationRule in ValidationRulesList)
            {
                int validationResult = validationRule.Validate(this._bankDetails);
                if (validationResult != 0)
                    return validationResult;
            }
            return 0;
        }
    }
}
