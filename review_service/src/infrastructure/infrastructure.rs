extern crate postgres;

use postgres::{Client, NoTls};

use crate::domain::domain::*;


pub struct ReviewRepository {

}

impl Repository for ReviewRepository {
    fn get_by_specification(self, condition: &str) -> Vec<Review> {
        todo!();
    }
}

impl ReviewRepository {
    pub fn seed(self) {
        let mut conn = Client::connect("postgresql://postgres:postgres@localhost:5433", NoTls)
            .unwrap();

        let table_existing = conn.query_one("SELECT EXISTS ( SELECT 1 FROM pg_tables WHERE tablename = 'reviews' ) AS table_existence;", &[]);

        let table_exists: bool = table_existing.unwrap().get(0);

        if table_exists {
            return;
        }

        conn.execute("CREATE TABLE reviews (
                        id              SERIAL PRIMARY KEY,
                        title           VARCHAR NOT NULL,
                        content         VARCHAR,
                        advertisement_id INT,
                        user_id         INT,
                        stars           INT,
                        helpful_review_votes INT,
                        is_deleted      BOOL,
                        create_date     DATE,
                        modification_date DATE,
                        deletion_date   DATE
                    )", &[]).unwrap();
        let review = Review {
            id: None,
            title: String::from("Test review"),
            content: String::from("Test content"),
            advertisement: Advertisement { id: Some(1) },
            stars: 4,
            user: User{ id: Some(1), username: "Test user".to_string(), total_helpful_review_votes: 0 },
            helpful_review_votes: 0,
            is_deleted: false,
            create_date: Default::default(),
            modification_date: Default::default(),
            deletion_date: Default::default(),
        };
        conn.execute("INSERT INTO reviews (title, content, advertisement_id, user_id, stars, helpful_review_votes, is_deleted, create_date, 
        modification_date, deletion_date) VALUES ($1, $2, $3, $4, $5, $6, $7, now(), now(), now())",
                    &[&review.title, &review.content, &review.advertisement.id, &review.user.id, &review.stars, &review.helpful_review_votes,
                    &review.is_deleted]).unwrap();

    }

    pub fn get_by_advertisement(self, advertisement_id: u32) -> Vec<Review> {
        let mut conn = Client::connect("postgresql://postgres:postgres@localhost:5433", NoTls)
            .unwrap();

        // todo: remove sql injection vulnerability
        let data = conn.query(&format!("SELECT id, title, content, advertisement_id, user_id, stars, helpful_review_votes, create_date, modification_date FROM reviews WHERE advertisement_id = {}", advertisement_id),
                &[]).unwrap();

        let mut result: Vec<Review> = vec![];

        for d in data {
            let review = Review {
                id: Some(d.get(0)),
                title: d.get(1),
                content: d.get(2),
                advertisement: Advertisement {
                    id: d.get(3)
                },
                user: User {
                    id: d.get(4),
                    username: String::from("Test user"),
                    total_helpful_review_votes: 42
                },
                stars: d.get(5),
                helpful_review_votes: d.get(6),
                is_deleted: false,
                create_date: Default::default(),
                modification_date: Default::default(),
                deletion_date: Default::default(),
            };
            result.push(review);
        }

        result
    }

    pub fn create(self, review: Review) {
        let mut conn = Client::connect("postgresql://postgres:postgres@localhost:5433", NoTls)
            .unwrap();

        // todo: remove sql injection vulnerability
        let sql_command = format!("INSERT INTO reviews(title, content, advertisement_id, user_id, stars, helpful_review_votes, is_deleted, create_date, modification_date, deletion_date) VALUES ('{}', '{}', {}, {}, {}, {}, false, now(), now(), null)", 
                        &review.title, &review.content, &review.advertisement.id.unwrap(), &review.user.id.unwrap(), &review.stars, &review.helpful_review_votes);
        conn.execute(&sql_command,
                &[]).unwrap();
    }

    
    pub fn update(self, review: Review) {
        let mut conn = Client::connect("postgresql://postgres:postgres@localhost:5433", NoTls)
            .unwrap();

        // todo: remove sql injection vulnerability
        let sql_command = format!("UPDATE reviews SET title = '{}', content = '{}', advertisement_id = {}, user_id = {}, stars = {}, helpful_review_votes = {}, is_deleted = {}, create_date = '{}', modification_date = '{}', deletion_date = '{}' WHERE id = {}", 
                        &review.title, &review.content, &review.advertisement.id.unwrap(), &review.user.id.unwrap(), &review.stars, &review.helpful_review_votes, &review.is_deleted, &review.create_date, &review.modification_date, &review.deletion_date, &review.id.unwrap());
        conn.execute(&sql_command,
                &[]).unwrap();        
    }

    
    pub fn delete(self, review_id: u32) {
        let mut conn = Client::connect("postgresql://postgres:postgres@localhost:5433", NoTls)
            .unwrap();

        let sql_command = format!("DELETE FROM reviews WHERE id = {}", &review_id);
        conn.execute(&sql_command,
                &[]).unwrap();        
    }
}