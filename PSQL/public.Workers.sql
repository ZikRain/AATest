-- Table: public.Workers

DROP TABLE IF EXISTS public.Workers;

CREATE TABLE IF NOT EXISTS public.Workers
(
    ID BIGSERIAL NOT NULL,
    FirstName CHARACTER VARYING(128) NOT NULL,
    LastName CHARACTER VARYING(128) NOT NULL,
    MiddleName CHARACTER VARYING(128) NOT NULL,
    Birthday DATE NOT NULL,
    Sex SMALLINT,
    HaveChildren BOOLEAN,
    CONSTRAINT PK_Workers PRIMARY KEY (ID)
);


CREATE INDEX IF NOT EXISTS ind_workers_birthday
    ON public.Workers USING btree
    (Birthday ASC NULLS LAST)
    TABLESPACE pg_default;


CREATE INDEX IF NOT EXISTS ind_workers_flm
    ON public.Workers USING btree
    (FirstName COLLATE pg_catalog.default ASC NULLS LAST,
	 LastName COLLATE pg_catalog.default ASC NULLS LAST,
	 MiddleName COLLATE pg_catalog.default ASC NULLS LAST)
    TABLESPACE pg_default;


CREATE INDEX IF NOT EXISTS ind_workers_options
    ON public.Workers USING btree
    (Sex ASC NULLS LAST, HaveChildren ASC NULLS LAST)
    TABLESPACE pg_default;