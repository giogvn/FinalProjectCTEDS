
CREATE DATABASE [SaveFirst];
GO

USE [SaveFirst];
GO

CREATE TABLE Saver(
	id INT PRIMARY KEY IDENTITY(1,1),
	email VARCHAR(255) NOT NULL UNIQUE,
    	type VARCHAR(255),
	payer_id INT,
	name	VARCHAR(255) NOT NULL,
	birthdate DATE
);

GO

CREATE TABLE Expense(
  id INT PRIMARY KEY IDENTITY(1,1),
  saver_id INT NOT NULL,
  expense_date DATE,
  due_date DATE,
  value INT NOT NULL,
  expense_type VARCHAR(255),
  description VARCHAR(255),
  status VARCHAR(255),
  number_of_installments INT DEFAULT 1,
  installment_value INT NOT NULL,
  installments_left INT DEFAULT 1,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

GO
CREATE TABLE Category(
  id INT PRIMARY KEY IDENTITY(1,1),
  saver_id INT,
  name VARCHAR(255) NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

GO

CREATE TABLE ExpenseCategory(
  expense_id INT,
  category_id INT,
  PRIMARY KEY (expense_id, category_id),
  FOREIGN KEY (expense_id) REFERENCES Expense(id),
  FOREIGN KEY (category_id) REFERENCES Category(id)
);

GO

CREATE TABLE SaverFinancialProduct(
  id INT PRIMARY KEY IDENTITY(1,1),
  saver_id INT,
  financial_product_name VARCHAR(255),
  recurrence VARCHAR(255),
  reason VARCHAR(255),
  purchase_date DATE,
  number_of_shares INT NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id),
);

GO

CREATE TABLE PaymentMethod(
  id INT PRIMARY KEY IDENTITY(1,1),
  saver_id INT,
  payment_method_id INT,
  name VARCHAR(255),
  type VARCHAR(255),
  bank VARCHAR(255),
  invoice_due_date DATE NOT NULL,
  invoice_closing_date DATE NOT NULL,
  registration_date DATE NOT NULL,
  cancel_date DATE DEFAULT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id),
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id)
);

GO

CREATE TABLE ExpensePaymentMethod(
  expense_id INT,
  payment_method_id INT,
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id)
);

GO

CREATE TABLE IncomeResource(
  id INT PRIMARY KEY IDENTITY(1,1),
  saver_id INT,
  value INT NOT NULL,
  name VARCHAR(255) NOT NULL,
  payday DATE NOT NULL,
  start_date DATE,
  end_date DATE,
  recurrence VARCHAR(255) NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

GO

CREATE TABLE PaymentMethodIncomeResource(
  payment_method_id INT,
  income_resource_id INT,
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id),
  FOREIGN KEY (income_resource_id) REFERENCES IncomeResource(id)
);
