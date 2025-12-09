This website contains 5 pages:
	- A Login page where it is possible to login to the website using a combination of a password and either an email or a username. After doing so, you get redirected to the Upload page.
	- A Register page where it is possible to create a user by entering an email, a username and a password. After doing so you get redirected to the Upload page.
	- A page to display a list of every user that has been created on the website. You can enter this page without being logged in but only the email and the username of the users are displayed, so you still cannot log into a different account unless you have their password.
	- An Upload page where a user that is logged in is able to enter a title and a description along with choosing a picture to upload to the gallery. When doing this, you get redirected to the Gallery page. On this upload page the user can also click on the "Hent antal billeder" button and will then be shown the amount of pictures in the gallery. This is an AJAX call.
	- A Gallery page where a logged in user can see a collection of all the pictures that have been uploaded to the website, along with the title and description of each picture.

As a whole, the website is built up using the MVC template and therefore it has multiple models, controllers and views (these use Razor Pages). There are 3 different controllers:
	- AccountController.cs: This controls the logic regarding both the Login page and the Register page.
	- UsersController.cs: This controls the logic regarding displaying the entire list of users in the database.
	- ImagesController.cs: This controls the logic regarding uploading pictures, the AJAX call and the displaying of pictures in the Gallery page.

The page is responsive in the way that when the window gets small enough, the background turns hotpink.

The website is protected from CXX by encrypting (hashing) every password and SQL injection by using the Entity Framework (uses parameterised queries).

The website uses RegEx on both the email and username field on the Register page. This is done in the UsersModel.cs.

Martin Koll Kronborg has made the Account part of the website, meaning the models, controller and views that have something to do with the Register and the Login parts of the website.

Emil Rune has made the Images part of the website, meaning the models, controller and views that have something to do with the Upload and Gallery parts of the website.

The Users page and the rest of the website/code has been made in collaboration.

To test out the website you can either create your own account and try it from there or you can use an already created account that we have created, specifically to be used for testing the website. The data for this account is the following (username and email can also be found under the "brugere" page as mentioned earlier):
	- Username: TestingUser
	- Email: test@test.dk
	- Password: Hacker