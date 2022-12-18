# SaveFirst
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

#### 3.1.2 Registering a Checking Account
![alt text](/Prints/RegisteringCheckingAccount.png)












While in the Main Windown, one can 


