USE [db_7];
GO

CREATE TABLE Saver(
	id VARCHAR(255) PRIMARY KEY,
	email VARCHAR(255) NOT NULL UNIQUE,
	type VARCHAR(255),
	password VARCHAR(255),
	payer_id VARCHAR(255),
	name	VARCHAR(255) NOT NULL,
	birthdate DATETIME
);

GO

CREATE TABLE Expense(
  id VARCHAR(255) PRIMARY KEY,
  saver_id VARCHAR(255) NOT NULL,
  expense_date DATETIME,
  due_date DATETIME,
  value FLOAT NOT NULL,
  expense_type VARCHAR(255),
  description VARCHAR(255),
  status VARCHAR(255),
  number_of_installments INT DEFAULT 1,
  installment_value FLOAT NOT NULL,
  installments_left INT DEFAULT 1,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

GO
CREATE TABLE Category(
  id VARCHAR(255) PRIMARY KEY,
  saver_id VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id)
);

GO

CREATE TABLE ExpenseCategory(
  expense_id VARCHAR(255),
  category_id VARCHAR(255), 
  PRIMARY KEY (expense_id, category_id),
  FOREIGN KEY (expense_id) REFERENCES Expense(id),
  FOREIGN KEY (category_id) REFERENCES Category(id)
);

GO

CREATE TABLE PaymentMethod(
  id VARCHAR(255) PRIMARY KEY,
  saver_id VARCHAR(255) NOT NULL,
  name VARCHAR(255),
  type VARCHAR(255),
  bank VARCHAR(255),
  limit FLOAT,
  invoice_due_date INT,
  invoice_closing_date INT,
  registration_date DATETIME NOT NULL,
  cancel_date DATETIME DEFAULT NULL,
  FOREIGN KEY (saver_id) REFERENCES Saver(id),
);

GO

CREATE TABLE ExpensePaymentMethod(
  expense_id VARCHAR(255),
  payment_method_id VARCHAR(255),
  PRIMARY KEY (expense_id, payment_method_id),
  FOREIGN KEY (payment_method_id) REFERENCES PaymentMethod(id),
  FOREIGN KEY (expense_id) REFERENCES Expense(id)
);