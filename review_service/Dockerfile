FROM rust:1.66.1

WORKDIR /usr/src/review_service
COPY . .

ENV ROCKET_ADDRESS=0.0.0.0

RUN cargo install --path .

CMD ["review_service"]
