use serde::{Serialize, Deserialize};
use chrono::NaiveDate;

trait Aggregate {
    fn apply(self, event: &impl DomainEvent);
}

trait Entity {
    fn get_id(self) -> Option<i32>;
}

trait ValueObject {

}

trait DomainEvent {

}

#[derive(Serialize, Deserialize)]
pub struct Review {
    pub id: Option<i32>,
    pub advertisement: Advertisement,
    pub user: User,
    pub title: String,
    pub content: String,
    pub stars: i32,
    pub helpful_review_votes: i32,
    pub is_deleted: bool,
    pub create_date: NaiveDate,
    pub modification_date: NaiveDate,
    pub deletion_date: NaiveDate,

}

#[derive(Serialize, Deserialize)]
pub struct User {
    pub id: Option<i32>,
    pub username: String,
    pub total_helpful_review_votes: i32
}

#[derive(Serialize, Deserialize)]
pub struct Advertisement {
    pub id: Option<i32>
}

impl Entity for User {
    fn get_id(self) -> Option<i32> {
        self.id
    }
}

impl Entity for Advertisement {
    fn get_id(self) -> Option<i32> {
        self.id
    }
}

struct HelpfulReviewVote {
    pub user: User,
    pub review_id: i32
}

impl ValueObject for HelpfulReviewVote {

}

impl Entity for Review {
    fn get_id(self) -> Option<i32> {
        self.id
    }
}

impl Review {
    fn apply(mut self, event: ReviewEvent) {
        match event {
            ReviewEvent::ReviewCreated { id } => self.id = Some(id),
            ReviewEvent::ReviewUpdated { review } => {
                self.title = review.title;
                self.content = review.content;
                self.stars = review.stars;
            },
            ReviewEvent::ReviewDeleted { id } => self.is_deleted = true,
            ReviewEvent::MarkReviewAsHelpful { id } => self.helpful_review_votes += 1,
        }
    }
}

enum ReviewEvent {
    ReviewCreated {
        id: i32
    },
    ReviewUpdated {
        review: Review
    },
    ReviewDeleted {
        id: i32
    },
    MarkReviewAsHelpful {
        id: i32
    }
}

impl DomainEvent for ReviewEvent {}

pub trait Repository {
    fn get_by_specification(self, condition: &str) -> Vec<Review>;
}
