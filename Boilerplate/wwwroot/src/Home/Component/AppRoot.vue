<template>
    <div>
        <nav-bar :app-name="'Document Archive'" :env="'DEV'"></nav-bar>
        <div class="container-fluid">
            <div class="row">
                <div class="col">
                    <router-view></router-view>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import Vue from 'vue'
    import navbar from './NavBar.vue'
    import axios from 'axios'
    import { RELOAD_TOKEN, LOGOUT } from '../actions'

    export default {
        components: {
            'nav-bar': navbar
        },
        created: async function () {
            //To login page incase 401
            axios.interceptors.response.use( (response) => {
                return response;
            }, (error) => {
                if (error.response.status == 401) {
                    this.$store.dispatch(LOGOUT);
                }
                return Promise.reject(error);
            });

            if (this.$store.getters.IsAuthenticated) {
                try {
                    await this.$store.dispatch(RELOAD_TOKEN);
                } catch (e) {
                    //Reload fail then logout
                    await this.$store.dispatch(LOGOUT);
                }
            }
        },
        data() {
            return {
            }
        }
    }
</script>
<style>

</style>