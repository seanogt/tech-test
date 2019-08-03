BEGIN
    INSERT INTO customers (customer_id, country, date_of_birth, name)
    VALUES (
           @customer_id,
            @country,
            @date_of_birth,
            @name
           )
END;