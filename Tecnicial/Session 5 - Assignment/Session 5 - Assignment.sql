use StoreDB
--1.Write a query that classifies all products into price categories:
SELECT 
    product_name,
    list_price,
    CASE
        WHEN list_price < 300 THEN 'Economy'
        WHEN list_price BETWEEN 300 AND 999 THEN 'Standard'
        WHEN list_price BETWEEN 1000 AND 2499 THEN 'Premium'
        WHEN list_price >= 2500 THEN 'Luxury'
    END AS price_category
FROM production.products;

--2.Create a query that shows order processing information with user-friendly status descriptions
SELECT 
    order_id,
    order_date,
    CASE order_status
        WHEN 1 THEN 'Order Received'
        WHEN 2 THEN 'In Preparation'
        WHEN 3 THEN 'Order Cancelled'
        WHEN 4 THEN 'Order Delivered'
    END AS status_description,
    CASE 
        WHEN order_status = 1 AND DATEDIFF(DAY, order_date, GETDATE()) > 5 THEN 'URGENT'
        WHEN order_status = 2 AND DATEDIFF(DAY, order_date, GETDATE()) > 3 THEN 'HIGH'
        ELSE 'NORMAL'
    END AS priority_level
FROM sales.orders;

-- 3.Write a query that categorizes staff based on the number of orders they've handled:
WITH StaffOrders AS (
    SELECT 
        staff_id,
        COUNT(order_id) AS order_count
    FROM sales.orders
    GROUP BY staff_id
)
SELECT 
    s.first_name + ' ' + s.last_name AS staff_name,
    so.order_count,
    CASE
        WHEN so.order_count = 0 THEN 'New Staff'
        WHEN so.order_count BETWEEN 1 AND 10 THEN 'Junior Staff'
        WHEN so.order_count BETWEEN 11 AND 25 THEN 'Senior Staff'
        WHEN so.order_count > 25 THEN 'Expert Staff'
    END AS staff_category
FROM sales.staffs s
LEFT JOIN StaffOrders so ON s.staff_id = so.staff_id;

--4.Create a query that handles missing customer contact information:
SELECT 
    first_name + ' ' + last_name AS customer_name,
    ISNULL(phone, 'Phone Not Available') AS phone,
    COALESCE(phone, email, 'No Contact Method') AS preferred_contact
FROM sales.customers;

-- 5.Write a query that safely calculates price per unit in stock:
SELECT 
    p.product_name,
    s.store_id,
    s.quantity,
    ISNULL(s.quantity, 0) AS safe_quantity,
    CASE 
        WHEN ISNULL(s.quantity, 0) = 0 THEN 'Out of Stock'
        ELSE 'In Stock'
    END AS stock_status,
    p.list_price / NULLIF(s.quantity, 0) AS price_per_unit
FROM production.products p
JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.store_id = 1;

-- 6.Create a query that formats complete addresses safely:
SELECT 
    first_name + ' ' + last_name AS customer_name,
    COALESCE(
        street + ', ' + city + ', ' + state + ' ' + COALESCE(zip_code, 'N/A'), 
        'Address Not Available'
    ) AS formatted_address
FROM sales.customers;

--7.Use a CTE to find customers who have spent more than $1,500 total:
WITH CustomerSpending AS (
    SELECT 
        o.customer_id,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY o.customer_id
    HAVING SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 1500
)
SELECT 
    c.customer_id,
    c.first_name + ' ' + c.last_name AS customer_name,
    cs.total_spent
FROM sales.customers c
JOIN CustomerSpending cs ON c.customer_id = cs.customer_id
ORDER BY cs.total_spent DESC;

--8.Create a multi-CTE query for category analysis:
WITH CategoryRevenue AS (
    SELECT 
        c.category_id,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue
    FROM production.categories c
    JOIN production.products p ON c.category_id = p.category_id
    JOIN sales.order_items oi ON p.product_id = oi.product_id
    GROUP BY c.category_id
),
CategoryAvgOrder AS (
    SELECT 
        c.category_id,
        AVG(oi.quantity * oi.list_price * (1 - oi.discount)) AS avg_order_value
    FROM production.categories c
    JOIN production.products p ON c.category_id = p.category_id
    JOIN sales.order_items oi ON p.product_id = oi.product_id
    GROUP BY c.category_id
)
SELECT 
    c.category_name,
    cr.total_revenue,
    cao.avg_order_value,
    CASE
        WHEN cr.total_revenue > 50000 THEN 'Excellent'
        WHEN cr.total_revenue > 20000 THEN 'Good'
        ELSE 'Needs Improvement'
    END AS performance_rating
FROM production.categories c
JOIN CategoryRevenue cr ON c.category_id = cr.category_id
JOIN CategoryAvgOrder cao ON c.category_id = cao.category_id;

--9.Use CTEs to analyze monthly sales trends:
WITH MonthlySales AS (
    SELECT 
        YEAR(order_date) AS order_year,
        MONTH(order_date) AS order_month,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS monthly_sales
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY YEAR(order_date), MONTH(order_date)
),
SalesWithPrevious AS (
    SELECT 
        *,
        LAG(monthly_sales) OVER (ORDER BY order_year, order_month) AS prev_month_sales
    FROM MonthlySales
)
SELECT 
    order_year,
    order_month,
    monthly_sales,
    ROUND(((monthly_sales - prev_month_sales) / NULLIF(prev_month_sales, 0)) * 100, 2) AS growth_pct
FROM SalesWithPrevious;

--10.Create a query that ranks products within each category:
WITH RankedProducts AS (
    SELECT 
        c.category_name,
        p.product_name,
        p.list_price,
        ROW_NUMBER() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS row_num,
        RANK() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS rank,
        DENSE_RANK() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS dense_rank
    FROM production.products p
    JOIN production.categories c ON p.category_id = c.category_id
)
SELECT *
FROM RankedProducts
WHERE row_num <= 3;

--11.Rank customers by their total spending:
WITH CustomerSpending AS (
    SELECT 
        c.customer_id,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent,
        RANK() OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS spending_rank,
        NTILE(5) OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS spending_group
    FROM sales.customers c
    JOIN sales.orders o ON c.customer_id = o.customer_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY c.customer_id
)
SELECT 
    c.first_name + ' ' + c.last_name AS customer_name,
    cs.total_spent,
    cs.spending_rank,
    CASE cs.spending_group
        WHEN 1 THEN 'VIP'
        WHEN 2 THEN 'Gold'
        WHEN 3 THEN 'Silver'
        WHEN 4 THEN 'Bronze'
        WHEN 5 THEN 'Standard'
    END AS customer_tier
FROM CustomerSpending cs
JOIN sales.customers c ON cs.customer_id = c.customer_id;

--12.Create a comprehensive store performance ranking:
WITH StoreMetrics AS (
    SELECT 
        s.store_id,
        s.store_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue,
        COUNT(DISTINCT o.order_id) AS order_count
    FROM sales.stores s
    JOIN sales.orders o ON s.store_id = o.store_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY s.store_id, s.store_name
)
SELECT 
    store_name,
    total_revenue,
    RANK() OVER (ORDER BY total_revenue DESC) AS revenue_rank,
    order_count,
    RANK() OVER (ORDER BY order_count DESC) AS order_rank,
    PERCENT_RANK() OVER (ORDER BY total_revenue) AS revenue_percentile
FROM StoreMetrics;

--13.Create a PIVOT table showing product counts by category and brand:
SELECT *
FROM (
    SELECT 
        c.category_name,
        b.brand_name,
        p.product_id
    FROM production.products p
    JOIN production.categories c ON p.category_id = c.category_id
    JOIN production.brands b ON p.brand_id = b.brand_id
) AS src
PIVOT (
    COUNT(product_id)
    FOR brand_name IN ([Electra], [Haro], [Trek], [Surly])
) AS pvt;

--14.Create a PIVOT showing monthly sales revenue by store:
SELECT *
FROM (
    SELECT 
        s.store_name,
        FORMAT(o.order_date, 'MMM') AS order_month,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS monthly_revenue
    FROM sales.stores s
    JOIN sales.orders o ON s.store_id = o.store_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY s.store_name, FORMAT(o.order_date, 'MMM')
) AS src
PIVOT (
    SUM(monthly_revenue)
    FOR order_month IN ([Jan], [Feb], [Mar], [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec])
) AS pvt;

--15.PIVOT order statuses across stores:
SELECT *
FROM (
    SELECT 
        s.store_name,
        CASE o.order_status
            WHEN 1 THEN 'Pending'
            WHEN 2 THEN 'Processing'
            WHEN 3 THEN 'Rejected'
            WHEN 4 THEN 'Completed'
        END AS status_name,
        o.order_id
    FROM sales.stores s
    JOIN sales.orders o ON s.store_id = o.store_id
) AS src
PIVOT (
    COUNT(order_id)
    FOR status_name IN ([Pending], [Processing], [Rejected], [Completed])
) AS pvt;

-- 16.Create a PIVOT comparing sales across years:
WITH YearlySales AS (
    SELECT 
        b.brand_name,
        YEAR(o.order_date) AS order_year,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS yearly_revenue
    FROM production.brands b
    JOIN production.products p ON b.brand_id = p.brand_id
    JOIN sales.order_items oi ON p.product_id = oi.product_id
    JOIN sales.orders o ON oi.order_id = o.order_id
    GROUP BY b.brand_name, YEAR(o.order_date)
),
Pivoted AS (
    SELECT *
    FROM YearlySales
    PIVOT (
        SUM(yearly_revenue)
        FOR order_year IN ([2016], [2017], [2018])
    ) AS pvt
)
SELECT 
    brand_name,
    [2016],
    [2017],
    [2018],
    ROUND(([2017] - [2016]) / NULLIF([2016], 0) * 100, 2) AS growth_2017,
    ROUND(([2018] - [2017]) / NULLIF([2017], 0) * 100, 2) AS growth_2018
FROM Pivoted;

--17.Use UNION to combine different product availability statuses:
-- In-stock products
SELECT p.product_id, p.product_name, 'In Stock' AS availability
FROM production.products p
JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.quantity > 0

UNION

-- Out-of-stock products
SELECT p.product_id, p.product_name, 'Out of Stock'
FROM production.products p
JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.quantity = 0 OR s.quantity IS NULL

UNION

-- Discontinued products
SELECT p.product_id, p.product_name, 'Discontinued'
FROM production.products p
LEFT JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.product_id IS NULL;

-- 18.Use INTERSECT to find loyal customers:
SELECT c.customer_id, c.first_name + ' ' + c.last_name AS customer_name
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2017

INTERSECT

SELECT c.customer_id, c.first_name + ' ' + c.last_name
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2018;

--19.Use multiple set operators to analyze product distribution:
-- Products in all stores
SELECT p.product_id, p.product_name, 'Available in All Stores' AS distribution
FROM production.products p
WHERE NOT EXISTS (
    SELECT store_id 
    FROM sales.stores 
    WHERE store_id NOT IN (
        SELECT store_id 
        FROM production.stocks 
        WHERE product_id = p.product_id
    )
)

UNION

-- Products in store1 but not store2
SELECT p.product_id, p.product_name, 'Only in Store 1'
FROM production.products p
WHERE EXISTS (
    SELECT 1 
    FROM production.stocks 
    WHERE product_id = p.product_id AND store_id = 1
)
AND NOT EXISTS (
    SELECT 1 
    FROM production.stocks 
    WHERE product_id = p.product_id AND store_id = 2
)

UNION

-- Products in store2 but not store1
SELECT p.product_id, p.product_name, 'Only in Store 2'
FROM production.products p
WHERE EXISTS (
    SELECT 1 
    FROM production.stocks 
    WHERE product_id = p.product_id AND store_id = 2
)
AND NOT EXISTS (
    SELECT 1 
    FROM production.stocks 
    WHERE product_id = p.product_id AND store_id = 1
);

-- 20.Complex set operations for customer retention:
-- Lost customers (2016 only)
SELECT c.customer_id, c.first_name + ' ' + c.last_name AS customer_name, 'Lost' AS status
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2016
AND c.customer_id NOT IN (
    SELECT customer_id 
    FROM sales.orders 
    WHERE YEAR(order_date) = 2017
)

UNION ALL

-- New customers (2017 only)
SELECT c.customer_id, c.first_name + ' ' + c.last_name, 'New'
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2017
AND c.customer_id NOT IN (
    SELECT customer_id 
    FROM sales.orders 
    WHERE YEAR(order_date) = 2016
)

UNION ALL

-- Retained customers (both years)
SELECT c.customer_id, c.first_name + ' ' + c.last_name, 'Retained'
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2016
AND c.customer_id IN (
    SELECT customer_id 
    FROM sales.orders 
    WHERE YEAR(order_date) = 2017
);