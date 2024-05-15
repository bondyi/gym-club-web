/*==============================================================*/
/* DBMS name:      PostgreSQL 9.x                               */
/* Created on:     15.05.2024 01:39:00                          */
/*==============================================================*/


/*==============================================================*/
/* Table: active_memberships                                    */
/*==============================================================*/
create table active_memberships (
   active_membership_id SERIAL               not null,
   user_id              INT4                 not null,
   visits_left          INT4                 null
      constraint CKC_VISITS_LEFT_ACTIVE_M check (visits_left is null or (visits_left >= 1)),
   end_time             TIMESTAMP WITH TIME ZONE null,
   constraint PK_ACTIVE_MEMBERSHIPS primary key (active_membership_id)
);

/*==============================================================*/
/* Index: active_memberships_PK                                 */
/*==============================================================*/
create unique index active_memberships_PK on active_memberships (
active_membership_id
);

/*==============================================================*/
/* Index: has_FK                                                */
/*==============================================================*/
create  index has_FK on active_memberships (
user_id
);

/*==============================================================*/
/* Table: attendence_marks                                      */
/*==============================================================*/
create table attendence_marks (
   attendence_mark_id   SERIAL               not null,
   user_id              INT4                 not null,
   created_at           TIMESTAMP WITH TIME ZONE not null default CURRENT_TIMESTAMP,
   constraint PK_ATTENDENCE_MARKS primary key (attendence_mark_id)
);

/*==============================================================*/
/* Index: attendence_marks_PK                                   */
/*==============================================================*/
create unique index attendence_marks_PK on attendence_marks (
attendence_mark_id
);

/*==============================================================*/
/* Index: makes_FK                                              */
/*==============================================================*/
create  index makes_FK on attendence_marks (
user_id
);

/*==============================================================*/
/* Table: exercise_workout                                      */
/*==============================================================*/
create table exercise_workout (
   exercise_workout_id  SERIAL               not null,
   workout_id           INT4                 not null,
   exercise_id          INT4                 not null,
   "order"              INT4                 not null
      constraint CKC_ORDER_EXERCISE check ("order" >= 1),
   superset_mark        VARCHAR(16)          null
      constraint CKC_SUPERSET_MARK_EXERCISE check (superset_mark is null or (superset_mark in ('start','end'))),
   constraint PK_EXERCISE_WORKOUT primary key (exercise_workout_id)
);

/*==============================================================*/
/* Index: exercise_workout_PK                                   */
/*==============================================================*/
create unique index exercise_workout_PK on exercise_workout (
exercise_workout_id
);

/*==============================================================*/
/* Index: ew_workout_FK                                         */
/*==============================================================*/
create  index ew_workout_FK on exercise_workout (
workout_id
);

/*==============================================================*/
/* Index: ew_exercise_FK                                        */
/*==============================================================*/
create  index ew_exercise_FK on exercise_workout (
exercise_id
);

/*==============================================================*/
/* Table: exercises                                             */
/*==============================================================*/
create table exercises (
   exercise_id          SERIAL               not null,
   exercise_name        VARCHAR(36)          not null,
   description          VARCHAR(400)         null,
   constraint PK_EXERCISES primary key (exercise_id)
);

/*==============================================================*/
/* Index: exercises_PK                                          */
/*==============================================================*/
create unique index exercises_PK on exercises (
exercise_id
);

/*==============================================================*/
/* Table: lessons                                               */
/*==============================================================*/
create table lessons (
   lesson_id            SERIAL               not null,
   lesson_name          VARCHAR(32)          not null,
   start_time           TIMESTAMP WITH TIME ZONE not null,
   constraint PK_LESSONS primary key (lesson_id)
);

/*==============================================================*/
/* Index: lessons_PK                                            */
/*==============================================================*/
create unique index lessons_PK on lessons (
lesson_id
);

/*==============================================================*/
/* Table: membership_info                                       */
/*==============================================================*/
create table membership_info (
   product_id           INT4                 not null,
   membership_type      VARCHAR(32)          not null,
   duration             INT4                 null
      constraint CKC_DURATION_MEMBERSH check (duration is null or (duration >= 1)),
   visit_count          INT4                 null
      constraint CKC_VISIT_COUNT_MEMBERSH check (visit_count is null or (visit_count >= 1))
);

/*==============================================================*/
/* Index: product_membership_FK                                 */
/*==============================================================*/
create  index product_membership_FK on membership_info (
product_id
);

/*==============================================================*/
/* Table: products                                              */
/*==============================================================*/
create table products (
   product_id           SERIAL               not null,
   product_type         VARCHAR(16)          not null
      constraint CKC_PRODUCT_TYPE_PRODUCTS check (product_type in ('visit')),
   product_name         VARCHAR(32)          not null,
   description          VARCHAR(400)         null,
   price                MONEY                not null,
   constraint PK_PRODUCTS primary key (product_id)
);

/*==============================================================*/
/* Index: products_PK                                           */
/*==============================================================*/
create unique index products_PK on products (
product_id
);

/*==============================================================*/
/* Table: purchase_product                                      */
/*==============================================================*/
create table purchase_product (
   purchase_id          INT4                 not null,
   product_id           INT4                 not null,
   constraint PK_PURCHASE_PRODUCT primary key (purchase_id)
);

/*==============================================================*/
/* Index: contains_PK                                           */
/*==============================================================*/
create unique index contains_PK on purchase_product (
purchase_id
);

/*==============================================================*/
/* Index: contains_FK                                           */
/*==============================================================*/
create  index contains_FK on purchase_product (
product_id
);

/*==============================================================*/
/* Table: purchases                                             */
/*==============================================================*/
create table purchases (
   purchase_id          SERIAL               not null,
   user_id              INT4                 not null,
   purchase_time        TIMESTAMP WITH TIME ZONE not null default CURRENT_TIMESTAMP,
   amount               MONEY                not null,
   constraint PK_PURCHASES primary key (purchase_id)
);

/*==============================================================*/
/* Index: purchases_PK                                          */
/*==============================================================*/
create unique index purchases_PK on purchases (
purchase_id
);

/*==============================================================*/
/* Index: pays_FK                                               */
/*==============================================================*/
create  index pays_FK on purchases (
user_id
);

/*==============================================================*/
/* Table: reviews                                               */
/*==============================================================*/
create table reviews (
   review_id            SERIAL               not null,
   user_id              INT4                 not null,
   review_type          VARCHAR(16)          not null
      constraint CKC_REVIEW_TYPE_REVIEWS check (review_type in ('trainer','workout_program','product','service')),
   target_id            INT4                 not null,
   rating_value         INT4                 not null
      constraint CKC_RATING_VALUE_REVIEWS check (rating_value between 1 and 5),
   description          VARCHAR(400)         null,
   created_at           TIMESTAMP WITH TIME ZONE not null default CURRENT_TIMESTAMP,
   constraint PK_REVIEWS primary key (review_id)
);

/*==============================================================*/
/* Index: reviews_PK                                            */
/*==============================================================*/
create unique index reviews_PK on reviews (
review_id
);

/*==============================================================*/
/* Index: leaves_FK                                             */
/*==============================================================*/
create  index leaves_FK on reviews (
user_id
);

/*==============================================================*/
/* Table: user_lesson                                           */
/*==============================================================*/
create table user_lesson (
   lesson_id            INT4                 not null,
   user_id              INT4                 not null,
   constraint PK_USER_LESSON primary key (lesson_id, user_id)
);

/*==============================================================*/
/* Index: user_lesson_PK                                        */
/*==============================================================*/
create unique index user_lesson_PK on user_lesson (
lesson_id,
user_id
);

/*==============================================================*/
/* Index: user_lesson2_FK                                       */
/*==============================================================*/
create  index user_lesson2_FK on user_lesson (
user_id
);

/*==============================================================*/
/* Index: user_lesson_FK                                        */
/*==============================================================*/
create  index user_lesson_FK on user_lesson (
lesson_id
);

/*==============================================================*/
/* Table: user_progresses                                       */
/*==============================================================*/
create table user_progresses (
   user_progress_id     SERIAL               not null,
   user_id              INT4                 not null,
   exercise_workout_id  INT4                 not null,
   completion_time      TIMESTAMP WITH TIME ZONE not null default CURRENT_TIMESTAMP,
   constraint PK_USER_PROGRESSES primary key (user_progress_id)
);

/*==============================================================*/
/* Index: user_progresses_PK                                    */
/*==============================================================*/
create unique index user_progresses_PK on user_progresses (
user_progress_id
);

/*==============================================================*/
/* Index: tracks_FK                                             */
/*==============================================================*/
create  index tracks_FK on user_progresses (
user_id
);

/*==============================================================*/
/* Index: stores_FK                                             */
/*==============================================================*/
create  index stores_FK on user_progresses (
exercise_workout_id
);

/*==============================================================*/
/* Table: users                                                 */
/*==============================================================*/
create table users (
   user_id              SERIAL               not null,
   user_role            VARCHAR(10)          not null default 'client'
      constraint CKC_USER_ROLE_USERS check (user_role in ('client','trainer','admin')),
   phone_number         VARCHAR(13)          not null,
   hash_password        VARCHAR(120)         not null,
   name                 VARCHAR(16)          null,
   surname              VARCHAR(32)          null,
   birth_date           DATE                 null,
   gender               BOOL                 null,
   refresh_token        VARCHAR(120)         null,
   refresh_token_created_at TIMESTAMP WITH TIME ZONE null,
   created_at           TIMESTAMP WITH TIME ZONE not null default CURRENT_TIMESTAMP,
   constraint PK_USERS primary key (user_id)
);

/*==============================================================*/
/* Index: users_PK                                              */
/*==============================================================*/
create unique index users_PK on users (
user_id
);

/*==============================================================*/
/* Table: workout_program_workout                               */
/*==============================================================*/
create table workout_program_workout (
   workout_program_id   INT4                 not null,
   exercise_workout_id  INT4                 not null,
   "order"              INT4                 not null
      constraint CKC_ORDER_WORKOUT_ check ("order" >= 1)
);

/*==============================================================*/
/* Index: wp_wpw_FK                                             */
/*==============================================================*/
create  index wp_wpw_FK on workout_program_workout (
workout_program_id
);

/*==============================================================*/
/* Index: ew_wpw_FK                                             */
/*==============================================================*/
create  index ew_wpw_FK on workout_program_workout (
exercise_workout_id
);

/*==============================================================*/
/* Table: workout_programs                                      */
/*==============================================================*/
create table workout_programs (
   workout_program_id   SERIAL               not null,
   workout_program_type VARCHAR(12)          not null
      constraint CKC_WORKOUT_PROGRAM_T_WORKOUT_ check (workout_program_type in ('weight_loss','relief','mass_and_strength','universal')),
   workout_program_name VARCHAR(36)          not null,
   location             BOOL                 not null,
   gender               BOOL                 not null,
   training_method      VARCHAR(16)          not null default 'standard'
      constraint CKC_TRAINING_METHOD_WORKOUT_ check (training_method in ('standard','superset','round','combine')),
   workouts_per_week    INT4                 not null
      constraint CKC_WORKOUTS_PER_WEEK_WORKOUT_ check (workouts_per_week between 1 and 5),
   difficulty           INT4                 not null
      constraint CKC_DIFFICULTY_WORKOUT_ check (difficulty between 1 and 5),
   duration_in_weeks    INT4                 not null
      constraint CKC_DURATION_IN_WEEKS_WORKOUT_ check (duration_in_weeks >= 1),
   description          VARCHAR(400)         null,
   constraint PK_WORKOUT_PROGRAMS primary key (workout_program_id)
);

/*==============================================================*/
/* Index: workout_programs_PK                                   */
/*==============================================================*/
create unique index workout_programs_PK on workout_programs (
workout_program_id
);

/*==============================================================*/
/* Table: workouts                                              */
/*==============================================================*/
create table workouts (
   workout_id           SERIAL               not null,
   constraint PK_WORKOUTS primary key (workout_id)
);

/*==============================================================*/
/* Index: workouts_PK                                           */
/*==============================================================*/
create unique index workouts_PK on workouts (
workout_id
);

alter table active_memberships
   add constraint FK_ACTIVE_M_HAS_USERS foreign key (user_id)
      references users (user_id)
      on delete cascade on update restrict;

alter table attendence_marks
   add constraint FK_ATTENDEN_MAKES_USERS foreign key (user_id)
      references users (user_id)
      on delete cascade on update restrict;

alter table exercise_workout
   add constraint FK_EXERCISE_EW_EXERCI_EXERCISE foreign key (exercise_id)
      references exercises (exercise_id)
      on delete cascade on update restrict;

alter table exercise_workout
   add constraint FK_EXERCISE_EW_WORKOU_WORKOUTS foreign key (workout_id)
      references workouts (workout_id)
      on delete cascade on update restrict;

alter table membership_info
   add constraint FK_MEMBERSH_PRODUCT_M_PRODUCTS foreign key (product_id)
      references products (product_id)
      on delete cascade on update restrict;

alter table purchase_product
   add constraint FK_PURCHASE_CONTAINS_PRODUCTS foreign key (product_id)
      references products (product_id)
      on delete restrict on update restrict;

alter table purchase_product
   add constraint FK_PURCHASE_CONTAINS2_PURCHASE foreign key (purchase_id)
      references purchases (purchase_id)
      on delete restrict on update restrict;

alter table purchases
   add constraint FK_PURCHASE_PAYS_USERS foreign key (user_id)
      references users (user_id)
      on delete restrict on update restrict;

alter table reviews
   add constraint FK_REVIEWS_LEAVES_USERS foreign key (user_id)
      references users (user_id)
      on delete restrict on update restrict;

alter table user_lesson
   add constraint FK_USER_LES_USER_LESS_LESSONS foreign key (lesson_id)
      references lessons (lesson_id)
      on delete cascade on update restrict;

alter table user_lesson
   add constraint FK_USER_LES_USER_LESS_USERS foreign key (user_id)
      references users (user_id)
      on delete cascade on update restrict;

alter table user_progresses
   add constraint FK_USER_PRO_STORES_EXERCISE foreign key (exercise_workout_id)
      references exercise_workout (exercise_workout_id)
      on delete cascade on update restrict;

alter table user_progresses
   add constraint FK_USER_PRO_TRACKS_USERS foreign key (user_id)
      references users (user_id)
      on delete cascade on update restrict;

alter table workout_program_workout
   add constraint FK_WORKOUT__EW_WPW_EXERCISE foreign key (exercise_workout_id)
      references exercise_workout (exercise_workout_id)
      on delete cascade on update restrict;

alter table workout_program_workout
   add constraint FK_WORKOUT__WP_WPW_WORKOUT_ foreign key (workout_program_id)
      references workout_programs (workout_program_id)
      on delete cascade on update restrict;

