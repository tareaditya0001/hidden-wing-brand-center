# Database scripts

EF Core migrations are the primary schema mechanism.

`scripts/seed.sql` is a reference insert for the V1 Hidden Wing catalog. Development databases are normally seeded by EF `HasData`.

`scripts/cleanup.sql` removes V1 seed rows. Use only in disposable environments.
