<template>
    <nav class="navbar navbar-expand-lg bg-body-tertiary">
        <div class="container-fluid">
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <router-link class="nav-link" to="/">Home</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'bookings'}">Bokning</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'sports'}">Fotboll</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'forum'}">Forum</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'about'}">Om</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'createsportevent'}">Skapa Idrottsevenemang</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'register'}">Registrera</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link class="nav-link" :to="{name:'login'}">Logga in</router-link>
                    </li>
                    <!--<li v-if="showAdminBoard" class="nav-item">
        <router-link to="/admin" class="nav-link">Admin Board</router-link>
    </li>
    <li v-if="showModeratorBoard" class="nav-item">
        <router-link to="/mod" class="nav-link">Moderator Board</router-link>
    </li>
    <li class="nav-item">
        <router-link v-if="currentUser" to="/user" class="nav-link">User</router-link>
    </li>-->
                </ul>
                <!--<ul class="navbar-nav ml-auto">
                    <div v-if="!currentUser" class="nav-item">
                        <router-link to="/register" class="nav-link">
                            <font-awesome-icon icon="user-plus" /> Sign Up
                        </router-link>
                    </div>
                    <div v-if="!currentUser" class="nav-item">
                        <router-link to="/login" class="nav-link">
                            <font-awesome-icon icon="sign-in-alt" /> Login
                        </router-link>
                    </div>
                    <div v-if="currentUser" class="nav-item">
                        <router-link to="/profile" class="nav-link">
                            <font-awesome-icon icon="user" />
                            {{ currentUser.username }}
                        </router-link>
                    </div>
                    <div v-if="currentUser" class="nav-item">
                        <a class="nav-link" @click.prevent="logOut">
                            <font-awesome-icon icon="sign-out-alt" /> LogOut
                        </a>
                    </div>
                </ul>-->
            </div>
        </div>
    </nav>
    <div class="container">
        <router-view />
    </div>
</template>

<script>
    //export default {
    //    computed: {
    //        currentUser() {
    //            return this.$store.state.auth.user;
    //        },
    //        showAdminBoard() {
    //            if (this.currentUser && this.currentUser['roles']) {
    //                return this.currentUser['roles'].includes('ROLE_ADMIN');
    //            }

    //            return false;
    //        },
    //        showModeratorBoard() {
    //            if (this.currentUser && this.currentUser['roles']) {
    //                return this.currentUser['roles'].includes('ROLE_MODERATOR');
    //            }

    //            return false;
    //        }
    //    },
    //    methods: {
    //        logOut() {
    //            this.$store.dispatch('auth/logout');
    //            this.$router.push('/login');
    //        }
    //    }
    //};
</script>

<style lang="scss">
    @import "~bootstrap/scss/bootstrap.scss";


</style>


