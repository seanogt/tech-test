BEGIN
    INSERT INTO customers (customer_id, country, date_of_birth, name)
    VALUES (
           $1,
            $2,
            $3,
            $4
           )
END;