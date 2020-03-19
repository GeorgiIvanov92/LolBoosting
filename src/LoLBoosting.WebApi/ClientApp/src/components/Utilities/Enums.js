export const Server = {
    None: -1,
    EUW: 1,
    EUNE: 2,
    NA: 3,
    TR: 4,
    RU: 5,
    KR: 6
}

export const ValidationState = {
    NotStarted: 0,
    Validating: 1,
    Rejected: 2,
    Confirmed: 3,
};

export const OrderType = {
    UnDefined: 0,
    DivisionBoost: 1,
    GamesBoost: 2,
    WinsBoost: 3,
    DuoQWinsBoost: 4,
    DuoQGamesBoost: 5,
    Coaching: 6,
}

export const Images = [
    { id: 1, src: '../Images/approved.jpg', title: 'approved' },
    { id: 2, src: '../Images/rejected.jpg', title: 'rejected'}
];