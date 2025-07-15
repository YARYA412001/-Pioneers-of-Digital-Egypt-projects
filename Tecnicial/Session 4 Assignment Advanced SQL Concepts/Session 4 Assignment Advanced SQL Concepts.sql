USE StoreDB
--1)Count the total number of products in the database.
SELECT COUNT(*) AS TotalProduct 
FROM production.products

--2)Find the average, minimum, and maximum price of all products.
SELECT 
    AVG(list_price) AS AvgPrice,
    MIN(list_price) AS MinPrice,
    MAX(list_price) AS MaxPrice
FROM production.products;
--3)Count how many products are in each category.
SELECT 
    c.category_name, 
    COUNT(p.product_id) AS ProductCount
FROM production.categories c
LEFT JOIN production.products p ON c.category_id = p.category_id
GROUP BY c.category_name
--4)Find the total number of orders for each store.
SELECT 
    s.store_name, 
    COUNT(o.order_id) AS OrderCount
FROM sales.stores s
LEFT JOIN sales.orders o ON s.store_id = o.store_id
GROUP BY s.store_name;
--5)Show customer first names in UPPERCASE and last names in lowercase for the first 10 customers.
SELECT TOP 10
    UPPER(first_name) AS FirstNameUpper,
    LOWER(last_name) AS LastNameLower
FROM sales.customers
ORDER BY customer_id
--6)Get the length of each product name. Show product name and its length for the first 10 products.
SELECT 
    product_name, 
    LEN(product_name) AS NameLength
FROM production.products
ORDER BY product_id
OFFSET 0 ROWS FETCH NEXT 10 ROWS ONLY
--7)Format customer phone numbers to show only the area code (first 3 digits) for customers 1-15.
SELECT 
    customer_id,
    LEFT(phone, 3) AS AreaCode
FROM sales.customers
WHERE customer_id BETWEEN 1 AND 15;
--8)Show the current date and extract the year and month from order dates for orders 1-10.
SELECT 
    order_id,
    GETDATE() AS CurrentDate,
    YEAR(order_date) AS OrderYear,
    MONTH(order_date) AS OrderMonth
FROM sales.orders
WHERE order_id BETWEEN 1 AND 10;
--9)Join products with their categories. Show product name and category name for first 10 products.
SELECT TOP 10
    p.product_name, 
    c.category_name
FROM production.products p
INNER JOIN production.categories c ON p.category_id = c.category_id;

--10)Join customers with their orders. Show customer name and order date for first 10 orders.
SELECT TOP 10
    c.first_name + ' ' + c.last_name AS CustomerName,
    o.order_date
FROM sales.customers c
INNER JOIN sales.orders o ON c.customer_id = o.customer_id;

--11)Show all products with their brand names, even if some products don't have brands. Include product name, brand name (show 'No Brand' if null).
SELECT 
    p.product_name,
    COALESCE(b.brand_name, 'No Brand') AS BrandName
FROM production.products p
LEFT JOIN production.brands b ON p.brand_id = b.brand_id;

--12)Find products that cost more than the average product price. Show product name and price.
SELECT 
    product_name,
    list_price
FROM production.products
WHERE list_price > (SELECT AVG(list_price) FROM production.products);
--13)Find customers who have placed at least one order. Use a subquery with IN. Show customer_id and customer_name.
SELECT 
    customer_id,
    first_name + ' ' + last_name AS CustomerName
FROM sales.customers
WHERE customer_id IN (SELECT customer_id FROM sales.orders);
--14)For each customer, show their name and total number of orders using a subquery in the SELECT clause.
SELECT 
    first_name + ' ' + last_name AS CustomerName,
    (SELECT COUNT(*) 
     FROM sales.orders o 
     WHERE o.customer_id = c.customer_id) AS OrderCount
FROM sales.customers c;
--15)Create a simple view called easy_product_list that shows product name, category name, and price. Then write a query to select all products from this view where price > 100.
--A) Create a simple view called easy_product_list that shows product name, category name, and price.
CREATE VIEW easy_product_list AS
SELECT 
    p.product_name,
    c.category_name,
    p.list_price AS Price
FROM production.products p
INNER JOIN production.categories c ON p.category_id = c.category_id;
--B)write a query to select all products from this view where price > 100.
SELECT * 
FROM easy_product_list 
WHERE Price > 100;
--16)Create a view called customer_info that shows customer ID, full name (first + last), email, and city and state combined. Then use this view to find all customers from California (CA).
--A)Create a view called customer_info that shows customer ID, full name (first + last), email, and city and state combined
CREATE VIEW customer_info AS
SELECT 
    customer_id,
    first_name + ' ' + last_name AS FullName,
    email,
    city + ', ' + state AS CityState
FROM sales.customers;
--B)Then use this view to find all customers from California (CA).
SELECT * 
FROM customer_info 
WHERE CityState LIKE '%CA%';
--17)Find all products that cost between $50 and $200. Show product name and price, ordered by price from lowest to highest.
SELECT 
    product_name,
    list_price
FROM production.products
WHERE list_price BETWEEN 50 AND 200
ORDER BY list_price ASC;
--18)Count how many customers live in each state. Show state and customer count, ordered by count from highest to lowest.
SELECT 
    state,
    COUNT(*) AS CustomerCount
FROM sales.customers
GROUP BY state
ORDER BY CustomerCount DESC;
--19)Find the most expensive product in each category. Show category name, product name, and price.
WITH RankedProducts AS (
    SELECT 
        c.category_name,
        p.product_name,
        p.list_price,
        RANK() OVER (PARTITION BY p.category_id ORDER BY p.list_price DESC) AS PriceRank
    FROM production.products p
    JOIN production.categories c ON p.category_id = c.category_id
)
SELECT 
    category_name,
    product_name,
    list_price
FROM RankedProducts
WHERE PriceRank = 1;
--20)Show all stores and their cities, including the total number of orders from each store.
SELECT 
    s.store_name,
    s.city,
    COUNT(o.order_id) AS OrderCount
FROM sales.stores s
LEFT JOIN sales.orders o ON s.store_id = o.store_id
GROUP BY s.store_name, s.city;