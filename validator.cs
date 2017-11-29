using System;
using System.Collections.Generic;
using System.Text;

using EPACS.BankValidateResource.Common_PayOutResource;

/// Change History
/// --------------
/// 27/10/2009 SNK Added to PayoutResource. Restructured namespace. Removed redundant consts which were causing warnings
/// 

namespace EPACS.BankValidateResource.BankValidators
{
  public class BankValidator_AT_MN : BankValidatorBase, IBankValidator
  {
    private bool checkAccountNumber = true;
    private int accountNumberMinLength = 4;
    private int accountNumberMaxLength = 11;
    private string accountNumberPaddingChar = "0";
    private string accountNumberType = "n"; //n | a | an | <blank=an>

    private bool checkBankCode = true;
    private int bankCodeMinLength = 5;
    private int bankCodeMaxLength = 5;
    private string bankCodePaddingChar = "";
    private string bankCodeType = "n"; //n | a | an | <blank=an>

    private bool checkBranchCode = false;
    private int branchCodeMinLength = 0;
    private int branchCodeMaxLength = 0;
    private string branchCodePaddingChar = "";
    private string branchCodeType = "n"; //n | a | an | <blank=an>

    private bool checkCheckDigits = false;
    private int checkDigitsMinLength = 0;
    private int checkDigitsMaxLength = 0;
    private string checkDigitsPaddingChar = "";
    private string checkDigitsType = "n"; //n | a | an | <blank=an>

    public BankValidator_AT_MN(BankDetails bankDetails)
      : base(bankDetails)
    {
    }

    public int Validate()
    {
      // Check not null
      if (_bankDetails == null)
        return -907;

      // AccountNumber
      string accountNumber = ValidatorCommonFunctions.cleanupIBANString(_bankDetails.BankAccountNumber);
      if (checkAccountNumber)
      {
        if (accountNumber.Length < accountNumberMinLength ||
            accountNumber.Length > accountNumberMaxLength)
        {
          return -945;
        }
        else if (accountNumberPaddingChar != "" && accountNumber.Length <= accountNumberMaxLength)
        {
          accountNumber = accountNumber.PadLeft(accountNumberMaxLength, char.Parse(accountNumberPaddingChar));
        }

        if (!ValidatorCommonFunctions.checkAlphaAndOrNumeric(accountNumber, accountNumberType))
        {
          return -946;
        }
        _bankDetails.BankAccountNumber = accountNumber;
      }

      // bankCode
      string bankCode = ValidatorCommonFunctions.cleanupIBANString(_bankDetails.BankCode);
      if (checkBankCode)
      {
        if (bankCode.Length < bankCodeMinLength ||
            bankCode.Length > bankCodeMaxLength)
        {
          return -947;
        }
        else if (bankCodePaddingChar != "" && bankCode.Length <= bankCodeMaxLength)
        {
          bankCode = bankCode.PadLeft(bankCodeMaxLength, char.Parse(bankCodePaddingChar));
        }

        if (!ValidatorCommonFunctions.checkAlphaAndOrNumeric(bankCode, bankCodeType))
        {
          return -948;
        }
        _bankDetails.BankCode = bankCode;
      }

      // branchCode
      string branchCode = ValidatorCommonFunctions.cleanupIBANString(_bankDetails.BranchCode);
      if (checkBranchCode)
      {
        if (branchCode.Length < branchCodeMinLength ||
            branchCode.Length > branchCodeMaxLength)
        {
          return -949;
        }
        else if (branchCodePaddingChar != "" && branchCode.Length <= branchCodeMaxLength)
        {
          branchCode = branchCode.PadLeft(branchCodeMaxLength, char.Parse(branchCodePaddingChar));
        }

        if (!ValidatorCommonFunctions.checkAlphaAndOrNumeric(branchCode, branchCodeType))
        {
          return -950;
        }
        _bankDetails.BranchCode = branchCode;
      }

      // checkDigits
      string checkDigits = ValidatorCommonFunctions.cleanupIBANString(_bankDetails.CheckDigits.ToString());
      if (checkCheckDigits)
      {
        if (checkDigits.Length < checkDigitsMinLength ||
            checkDigits.Length > checkDigitsMaxLength)
        {
          return -951;
        }
        else if (checkDigitsPaddingChar != "" && checkDigits.Length <= checkDigitsMaxLength)
        {
          checkDigits = checkDigits.PadLeft(checkDigitsMaxLength, char.Parse(checkDigitsPaddingChar));
        }

        if (!ValidatorCommonFunctions.checkAlphaAndOrNumeric(checkDigits, checkDigitsType))
        {
          return -952;
        }
        _bankDetails.CheckDigits = int.Parse(checkDigits);
      }

      return 0;
    }
  }
}
