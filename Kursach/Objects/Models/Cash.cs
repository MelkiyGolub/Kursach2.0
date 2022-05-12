using Kursach.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace Kursach.Objects.Models;

[Table("Cash")]
public class Cash : Model
{
    private Cash(Guid Id, int balance, List<CashRecord> cashRecords)
    {
        ID = Id;
        CurrentBalance = balance;
        CashRecords = cashRecords;

        SqlModel.GetInstance().Insert(this);
    }

    public static Cash Instance { get; private set; } = null;

    [Column("Balance")]
    public int CurrentBalance { get; private set; }

    public List<CashRecord> CashRecords { get; set; }

    public void RecieveMoney(int sum, string comment)
    {
        CurrentBalance += sum;

        CashRecord record = new(sum, comment);

        CashRecords.Add(record);

        SqlModel db = SqlModel.GetInstance();

        db.Insert(record);
        db.Update(this);
    }

    public bool Pay(int sum, string comment)
    {
        if (CurrentBalance - sum < 0)
        {
            MessageBox.Show("В кассе слишком мало денег для такой оплаты");
            return false;
        }

        CurrentBalance -= sum;

        CashRecord record = new(sum, comment);

        CashRecords.Add(record);

        SqlModel db = SqlModel.GetInstance();

        db.Insert(record);
        db.Update(this);

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