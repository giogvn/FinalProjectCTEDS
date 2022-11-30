CREATE TABLE Saver(
	id INTEGER PRIMARY KEY AUTOINCREMENT,
	email TEXT NOT NULL,
    Saver_type TEXT CHECK(Saver_type = "payer" OR Saver_type = "dependent"),
	payer_id INTEGER,
	name	TEXT NOT NULL,
	birthdate DATE
);

CREATE TABLE Expense(
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  saver_id INTEGER NOT NULL,
  expense_date DATE,
  due_date DATE,
  value INTEGER NOT NULL,
  expense_type TEXT CHECK(expense_type = "recurrent" OR expense_type = "sporadic"),
  description TEXT,
  status TEXT CHECK (status = "paid" OR status = "active"),
  number_of_installments INTEGER DEFAULT 1,
  installment_value INTEGER NOT NULL,
  installments_left INTEGER DEFAULT 1,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

CREATE TABLE Category(
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  saver_id INTEGER,
  name TEXT NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

CREATE TABLE ExpenseCategory(
  expense_id INTEGER,
  category_id INTEGER,
  PRIMARY KEY (expense_id, category_id),
  FOREIGN KEY (expense_id) REFERENCES Expense(id),
  FOREIGN KEY (category_id) REFERENCES Category(id)
);

CREATE TABLE FinancialProduct(
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  name TEXT NOT NULL,
  price INTEGER NOT NULL
);


CREATE TABLE SaverFinancialProduct(
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  saver_id INTEGER,
  financial_product_id INTEGER,
  recurrence TEXT,
  reason TEXT,
  purchase_date DATE,
  number_of_shares INTEGER NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id),
  FOREIGN KEY (financial_product_id) REFERENCES FinancialProduct(id)
);

CREATE TABLE ExpensePaymentMethod(
  expense_id INTEGER,
  payment_method_id INTEGER,
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id)
);

CREATE TABLE PaymentMethod(
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  saver_id INTEGER,
  payment_method_id INTEGER,
  name TEXT,
  type TEXT,
  bank TEXT DEFAULT "undefinedBank",
  invoice_due_date DATE NOT NULL,
  invoice_closing_date DATE NOT NULL,
  registration_date DATE NOT NULL,
  cancel_date DATE DEFAULT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id),
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id)
);

CREATE TABLE IncomeResource(
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  saver_id INTEGER,
  value INTEGER NOT NULL,
  name TEXT NOT NULL,
  payday DATE NOT NULL,
  start_date DATE,
  end_date DATE,
  recurrence TEXT NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

CREATE TABLE PaymentMethodIncomeResource(
  payment_method_id INTEGER,
  income_resource_id INTEGER,
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id),
  FOREIGN KEY (income_resource_id) REFERENCES IncomeResource(id)
);
