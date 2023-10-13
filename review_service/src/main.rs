#[macro_use] extern crate rocket;

pub mod domain;
mod infrastructure;
use std::thread;
use rocket::{serde::json::{json, Value, Json}, response::status::Created};

use crate::{infrastructure::infrastructure::ReviewRepository, domain::domain::Review};

#[get("/seed")]
fn seed() -> &'static str {
    let repo = ReviewRepository {};
    thread::spawn(|| {
        repo.seed(); 
    }).join().expect("Thread panicked");

    "Hello, world!"
}

#[get("/<advertisement_id>")]
fn get_reviews(advertisement_id: u32) -> Value {
    let repo = ReviewRepository {};
    thread::spawn(move || {
        json!(repo.get_by_advertisement(advertisement_id))
    }).join().unwrap()
}

#[post("/", format = "application/json", data = "<review>")]
fn create_review(review: Json<Review>) {
    let repo = ReviewRepository {};
    thread::spawn(move || {
        json!(repo.create(review.into_inner()))
    }).join().unwrap();
}

#[put("/", format = "application/json", data = "<review>")]
fn update_review(review: Json<Review>) {
    let repo = ReviewRepository {};
    thread::spawn(move || {
        json!(repo.update(review.into_inner()))
    }).join().unwrap();
}

#[delete("/<review_id>")]
fn delete_review(review_id: u32) {
    let repo = ReviewRepository {};
    thread::spawn(move || {
        json!(repo.delete(review_id))
    }).join().unwrap();
}

#[launch]
fn rocket() -> _ {
    rocket::build()
        .mount("/seed", routes![seed])
        .mount("/", routes![get_reviews])
        .mount("/", routes![create_review])
        .mount("/", routes![update_review])
        .mount("/", routes![delete_review])
}

