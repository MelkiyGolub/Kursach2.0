using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace Kursach.Objects.Models;

[Table("Cash")]
public class Cash : Model
{
    static Cash()
    {
        InitializeCash(Guid.NewGuid(),387255, new());
    }

    private Cash(Guid Id, int balance, List<CashRecord> cashRecords)
    {
        ID = Id;
        CurrentBalance = balance;
        CashRecords = cashRecords;
    }

    public static Cash Instance { get; private set; }

    [Column("Balance")]
    public int CurrentBalance { get; private set; }

    public List<CashRecord> CashRecords { get; private init; }

    public void RecieveMoney(int sum, string comment)
    {
        CurrentBalance += sum;

        CashRecords.Add(new(sum, comment));
    }

    public bool Pay(int sum, string comment)
    {
        if (CurrentBalance - sum < 0)
        {
            MessageBox.Show("В кассе слишком мало денег для такой оплаты");
            return false;
        }

        CurrentBalance -= sum;

        CashRecords.Add(new(sum, comment));

        return true;
    }

    public static void InitializeCash(Guid ID, int balance, List<CashRecord> cashRecords)
    {
        if (Instance is null)
            Instance = new(ID, balance, cashRecords);
        else
            MessageBox.Show("Касса уже создана! Нельзя создать ещё раз");
    }
}