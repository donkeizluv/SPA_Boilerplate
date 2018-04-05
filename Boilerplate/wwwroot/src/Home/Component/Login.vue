<template id="loginTemplate">
    <div class="row h-100 justify-content-center align-items-center mt-5">
        <div class="col-md-4">
            <div class="card mt-5">
                <div class="card-header">
                    Đăng nhập
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-group">
                            <label>Tên</label>
                            <input class="form-control" type="text" v-model="Username"  v-on:keyup.enter="Login">
                        </div>
                        <div class="form-group">
                            <label>Mật khẩu</label>
                            <input class="form-control" type="password" v-model="Pwd"  v-on:keyup.enter="Login">
                        </div>
                        <p id="status" class="text-center text-danger" style="height: 15px;">{{Status}}</p>
                        <button v-bind:disabled="!CanSubmit" v-on:click="Login" class="btn btn-primary btn-lg float-right" style="width: 20%">
                            <span>Login</span>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    import { LOGIN } from '../actions'
    export default {
        name: 'login',
        template: '#loginTemplate',
        computed: {
            CanSubmit: function () {
                if (this.Username && this.Pwd)
                    return true;
                return false;
            }
        },
        data: function () {
            return {
                Username: '',
                Pwd: '',
                Status: '',
            }
        },
        methods: {
            Login: async function () {
                if (!this.CanSubmit) return;
                try {
                    await this.$store.dispatch(LOGIN, { username: this.Username, pwd: this.Pwd });
                } catch (e) {
                    //console.log(e);
                    this.Status = 'Đăng nhập thất bại';
                }
            }
        }
    }
</script>
<style scoped>
    .modal-dialog {
        margin: 20vh auto 0px auto
    }
</style>