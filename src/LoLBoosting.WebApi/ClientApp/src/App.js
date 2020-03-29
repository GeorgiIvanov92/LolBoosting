import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import { BoostMe } from './components/BoostMe';
import "./components/Backgrounds.css";

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
      super(props);
      this.state = {
          divClassName: '',
      }

      this.addClass = this.addClass.bind(this);
      this.removeClass = this.removeClass.bind(this);
    }

  render() {
        return (
            <div className={this.state.divClassName}>
                <Layout addClass={this.addClass}>
                    <Route exact path='/'  component={Home} />
                    <Route path='/boostme' component={BoostMe} />
                    <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
                </Layout>
            </div>
        );
    }


  addClass(className) {
      if (this.state.divClassName !== className) {
          this.setState({ divClassName: className });
      }
  }

    removeClass() {
        if (this.state.divClassName !== '') {
            this.setState({ divClassName: '' });
        }
    }
}
