create or replace function FullTextSearch(
   param character varying(128)
)
RETURNS SETOF public."Tours"
language sql    
as $$


SELECT *
FROM public."Tours" t
WHERE t."name" ~ param OR t."description" ~ param OR t."from" ~ param OR t."to" ~ param
OR t."id"=(SELECT "tid" FROM public."Logs" l WHERE l."comment" ~ param);

$$;