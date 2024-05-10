import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import AboutView from '../views/about/AboutView.vue'
import BookingView from '../views/booking/BookingView.vue'
import SoccerView from '../views/sports/SoccerView.vue'
import ForumView from '../views/forum/ForumView.vue'
import CreateSportEventViewVue from '../views/createsportevent/CreateSportEventView.vue'
import Login from "../components/Login.vue";
import Register from "../components/Register.vue";
//// lazy-loaded
//const Profile = () => import("./components/Profile.vue")
//const BoardAdmin = () => import("./components/BoardAdmin.vue")
//const BoardModerator = () => import("./components/BoardModerator.vue")
//const BoardUser = () => import("./components/BoardUser.vue")



const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView
    },
    {
        path: '/bookings',
        name: 'bookings',
        component: BookingView
    },
    {
        path: '/sports',
        name: 'sports',
        component: SoccerView
    },
    {
        path: '/forum',
        name: 'forum',
        component: ForumView
    },
    {
        path: '/about',
        name: 'about',
        component: AboutView
    },
    {
        path: '/createsportevent',
        name: 'createsportevent',
        component: CreateSportEventViewVue
    },
    //{
    //    path: "/profile",
    //    name: "profile",
    //    // lazy-loaded
    //    component: Profile,
    //},
    //{
    //    path: "/admin",
    //    name: "admin",
    //    // lazy-loaded
    //    component: BoardAdmin,
    //},
    //{
    //    path: "/mod",
    //    name: "moderator",
    //    // lazy-loaded
    //    component: BoardModerator,
    //},
    //{
    //    path: "/user",
    //    name: "user",
    //    // lazy-loaded
    //    component: BoardUser,
    //},
    {
        path: '/login',
        name: 'login',
        component: Login
    },
    {
        path: '/register',
        name: 'register',
        component: Register
    },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
