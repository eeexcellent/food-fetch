# PRODUCT ORDER MANAGEMENT SERVICE

This is a simple service providing the ability to order food, check status info about order, assign orders to users.

## User Stories

- As a user, I want to be able to watch the list of restaurants;
- As a user, I want to be able to watch the list of food by a restaurant;
- As a user, I want to be able to watch the
- As a user, I want to be able to create order.

## User Roles

- `client` - The role describing an ordinary customer that can make orders;
- `deliveryman` - The role describing deliveryman that can change the order status;

## Order Statuses

Orders can be in one of the following statuses:

- `preparing` - The order has been sent to a restaurant;
- `cooking` - The restaurant is cooking food describing in the order;
- `coming` - A deliveryman is on the way;
- `delivered` - The order has been successfully delivered;
- `failed` - The order has been failed due to deliveryman or restaurant.

## Order Fields

Tasks have the following fields:

- `id` - The unique identifier for the order;
- `restaurant` - The restaurant that prepares the food specified in the order;
- `order_list` - The list of dishes chosen by the client;
- `request` - The client's wish to a restaurant;
- `status` - The status of the order.
- `ordered_at` - The date and time the order was created;
- `closed_at` - The date and time the order was delivered/failed;
- `delivery_place` - The place of delivery;
- `email` - The email address of the client;

## Food Fields

Food have the following fields:

- `id` - The unique identifier for the food provided in the service;
- `title` - The title of the food;
- `descripion` - The description of the food provided by a restaurant;
- `availability` - The indicator that shows if food is available to order;
- `calories` - The number of calories in food;
- `restaurant` - The restaurant that prepares this food.

## Restaurant Fields

Restaurant have the following fields:

- `id` - The unique identifier for the restaurant provided in the service;
- `title` - The title of the restaurant;
- `descripion` - The description of the restaurant;
- `availability` - The indicator showing whether the restaurant is closed or open at the moment;
- `working_hours` - The time interval showing when the restaurant is open;
- `food_list` - The list of products that restaurant has.

## Users Fields

- `id` - The unique identifier of user;
- `first_name` - The first name of user;
- `second_name` - The second name of user;
- `email` - The email of user;
- `role` - The role of the user;

## Restaurant Endpoints

The following endpoints are available for interacting with restaurants:

- `GET /restaurants` - Retrieve a list of restaurants;
- `GET /restaurants/:id` - Retrieve an information about the restaurant by it's ID;
- `GET /restaurants/:id/food` - Retrieve an information about food list of the restaurant by it's ID;
- `GET /restaurants/:restaurant_id/food/:food_id` - Retrieve an information about a specific restaurant food by their ID;

## Order Endpoints

The following endpoints are available for interacting with restaurants:

- `PUT /order` - Create a new order;
- `GET /order/:id` - Retrieve an information about order by it's ID;
- `PATCH /order/:id/status` - Update the status of an order.

## Users Endpoints

The following endpoints are available for interacting with users:

- `PUT /users/new/:role` - Create a new user;
- `PATCH /users/:id` - Update an information about user by his ID;
