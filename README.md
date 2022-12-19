# SaveFirst's User Manual
SaveFirst is a Windows application made with the Windows Presentation Foundation (WPF) user interface structure.
The aplicastion's use cases are the following:

- Expenses Management by Categories: for the ones who wish a system where they can save their expenses separated by category
- Short-term Financial Planning: for the ones who wish a system where they can set a limit to each of one's payment method used to pay their expenses. This way the user can see how far they are from this limit before deciding to spend money using the corresponding payment method



## 1. Creating a User

Run the application using VisualStudio the application, the user will be presented to the following screen:

![alt text](/Prints/LoginWindow.png)

Click the **"Cadastro"** button to go to open the registration window.

![alt text](/Prints/RegisteringUser.png)

Fill in the textboxes:

- *Nome*: your name
- *Email*: your valid and yet not-registered e-mail
- *Senha*: any non-blank sequence of characters representing the user password

Click the "Efetuar Cadastro Button" twice. This will redirect to the **LoginWindow**.

## 2. Logging in

![alt text](/Prints/LoginWindow.png)

Fill in the textboxes:

- *Email*: your valid  and already registered e-mail
- *Senha*: you password

Click the "Acessar" button. This will open to the **MainWindow**.

## 3. Registering a Payment Method

Click the "Cadastrar Novo Método de Pagamento" button in the upper-right corner of the MainWindow.

![alt text](/Prints/RegisteringPaymentMethod.png)

This should open the **PaymentMethodRWindow**

### 3.1 Selecting one PaymentMethod type

While in the **PaymentMethodRWindow** click in the "Escolha uma opção" drop-down list. This should open a list with two entries:

![alt text](/Prints/SelectingPaymentMethodType.png)

Choose and click in one of the two options:

- *Cartão de Crédito*: if the PaymentMethod being registered is a Credit Card
- *Conta Corrente*: if the PaymentMethod being registered is a Checking Account

#### 3.1.1 Registering a Credit Card

If the "Cartão de Crédito" option was clicked, the **PaymentMethodRWindow** should be displayed as the following:

![alt text](/Prints/RegisteringCreditCard.png)

Fill in the textboxes:

- *Nome*: user-defined name for the Credit Card being registered
- *Banco*: name of the Credit Card's bank
- *Valor Limite*: very important field. This is the field that represents the User-defined limit to that PaymentMethod and is the target that will help him or her to decide whether to make another expense or not. In other words, it is the User-defined maximum value to be spent from that PaymentMethod in the current month. If the total value amount of active expenses registered to that PaymentMethod is greater that this limit, this PaymentMethod bar will appear red as described in the section **MainWindow**
- *Fechamento da fatura (dia)*: the Credit Card's invoice closing day 
- *Vencimento da fatura (dia)*: the Credit Card's invoice due day
- *Data de Validade*: the Credit Card's expiration month and year. The MM/YYYY format must be used.

Click the "Cadastrar método de pagamento" button. This should redirect the application to the **MainWindow**.

#### 3.1.2 Registering a Checking Account

**If the "Conta Corrente" option was clicked, the **PaymentMethodRWindow** should be displayed as the following:**

![alt text](/Prints/RegisteringCheckingAccount.png)

Fill in the textboxes:

- *Nome*: user-defined name for the Checking Account being registered
- *Banco*: name of the  Checking Account's bank
- *Valor Limite*: very important field. This is the field that represents the User-defined limit to that PaymentMethod and is the target that will help him or her to decide whether to make another expense or not. In other words, it is the User-defined maximum value to be spent from that PaymentMethod in the current month. If the total value amount of active expenses registered to that PaymentMethod is greater that this limit, this PaymentMethod bar will appear red as described in the section **MainWindow**


Click the "Cadastrar método de pagamento" button. This should redirect the application to the **MainWindow**.

## 4. Registering an Expense

While in the **MainWindow**, click the "Cadastrar Novo Gasto" button. This should open the **ExpenseRWindow**

![alt text](/Prints/RegisteringCheckingAccount.png)

Fill in the textboxes:

- *Descrição*: a brief sequence of characters describing the expense
- *Valor*: total Expense Value
- *Numero de Parcelas*: total expense installments.

Click the "Selecione uma data" date box and fill it with the Expense date.

### 4.1 Selecting Expense Type
Click the "Escolha o tipo de gasto" dropdown list. Choose one of the following options displayed:

![alt text](/Prints/SelectingExpenseType.png)

- *Esporádico*: if the expense is sporadic and don't have a periodic behavior (it won't happen in the following months with the same values)
- *Recorrente*: if the Expense is not sporadic. Common cases are "Gym" and "StreamingService" payments


### 4.1 Selecting Expense's PaymentMethod
Click the empty button below the "Escolha o tipo de gasto" dropdown list button . This should open a dropdown list with the already registered **PaymentMethods** for the current user.

Click on the **PaymentMethod** name displayed used for the Expense being registered or the "Cadastrar um novo método de pagamento" option, which should redirect the application the the **PaymentMethodRWindow** explained in the **Registering a Payment Method** previous section.

![alt text](/Prints/SelectingPaymentMethod.png)

### 4.1 Selecting Expense's Category

Click the empty button two positions below the "Escolha o tipo de gasto" dropdown list button . This should open a dropdown list with the already registered **Categories** for the current user.

Click on the **Criar uma nova categoria** option displayed in the dropdown list to register a new category if you want to register a new category.

#### 4.1.1 Registering a Category

![alt text](/Prints/SelectingPaymentMethod.png)

**Fill in the textbox:**
- *Nome*: the name of the category that must have not yet been registered by the current user.

Click the "Adicionar" button. This should redirect the application to the default **ExpenseRWindow**.

Click the "Adicionar gasto" button to finish the Expense registration. This should redirect the application to the **MainWindow**.

## 5. Main Window 

If the user have already registered some Expenses, Expense Categories and PaymentMethods his or her **MainWindow** should be similar to the following.

![alt text](/Prints/SomewhatPopulatedMainWindow.png)

### 5.1 PaymentMethod's Limits Status

The left portion of **MainWindow** displays how far each one of the Payment Methods registered by the user is from the its limit. The redder the bar for each PaymentMethod, the less the user should spend from it.


![alt text](/Prints/UserPaymentMethodSituation.png)

Some information for the user's PaymentMethods with the **MainWindow** above:

- He or she has already spent 1200 from a limit of 2000 set for the *Cartão 1* PaymentMethod


### 5.2 Total Expense's Values by Category

The right portion of **MainWindow** displays how much the user has spent in each category he or she has registered.


![alt text](/Prints/UserCategorySituation.png)

Some information for the user's PaymentMethods with the **MainWindow** above:

- He or she has spent an amout of 570 in the category "Entretenimento", which means that the Expenses from this category that are marked as "active" in the database sum 570. Being "active in the database" means that the expense has some installment that has not yet been paid. Therefore, this value is only valid for Expenses paid with credit card and whose last installment due date has not yet arrived. Being "active" or not is automatically defined by the database management system using the expense's due date  (which is automatically calculated) and the number of installments, both defined in the expense registration moment.


### 5.3 Navigation Buttons

![alt text](/Prints/NavigationButtons.png)

The highlighted butttons above are used to navigate through the right and left portions of the **MainWindow** explained in the previous 5.1 and 5.2 sections.

### 5.4 Total Expense's Values

![alt text](/Prints/UserMoneySituation.png)

The lower-right portion of **MainWindow** displays the following informations:

- *Você ainda tem R$XXX*: the sum of all the PaymentsMethod's limits registered minus the sum of all the Expenses currently marked as "active" in the database is equal to R$XXX. This value represents how much the user can still spend if he or she wants to respect every PaymentMethod's limits registered.  Being "active in the database" means that the expense has some installment that has not yet been paid. Therefore, this value is only valid for Expenses paid with credit card and whose last installment due date has not yet arrived. Being "active" or not is automatically defined by the database management system using the expense's due date  (which is automatically calculated) and the number of installments, both defined in the expense registration moment. 

- *Você gastou R$XXX* : the sum of all Expenses' installment values currently marked as "active" in the database is equal to R$XXX.


### 5.5 Expenses List

To list the currently active Expenses click the "Listar todos gastos" button.

![alt text](/Prints/ExpensesListButton.png)


This should open the **ExpenseLWindow**, displaying the active expenses for the user. Being "active in the database" means that the expense has some installment that has not yet been paid.

![alt text](/Prints/ExpenseList.png)

The user has one active expense with descrption "Academia" and total value of 120 to be paid in one installment ("Parcela" columnn).
